using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PluginContracts;
using OutputHelperLib;
using System.Threading;
using System.Threading.Tasks;


namespace CompareFrequencies
{
    public class CompareFrequencies : LinearPlugin
    {


        public string[] InputType { get; } = { "Frequency Lists" };
        public string OutputType { get; } = "Frequency Comparisons";

        public Dictionary<int, string> OutputHeaderData { get; set; } = new Dictionary<int, string>() { { 0, "None" } };
        public bool InheritHeader { get; } = false;

        #region Plugin Details and Info

        public string PluginName { get; } = "Compare Frequencies";
        public string PluginType { get; } = "Corpus Tools";
        public string PluginVersion { get; } = "1.1.01";
        public string PluginAuthor { get; } = "Ryan L. Boyd (ryan@ryanboyd.io)";
        public string PluginDescription { get; } = "Compare n-gram frequencies from two BUTTER frequency lists. This plugin will calculate metrics that help you to see and understand " +
                                                   "the relative n-gram differences between two copora. Metrics calculated include Log Likelihood (LL), %DIFF, Bayes Factors (BIC), " +
                                                   "Effect Size for Log Likelihood (ELL), Relative Risk (RRisk), Log Ratio, and Odds Ratio." + Environment.NewLine + Environment.NewLine +
                                                   "For quick testing heuristics, you can interpret the Log Likelihood (LL) score along the following p-value table:" + Environment.NewLine +
                                                    "\tLL >= 3.84, p < .05" + Environment.NewLine +
                                                    "\tLL >= 6.63, p < .01" + Environment.NewLine +
                                                    "\tLL >= 10.83, p < .001" + Environment.NewLine +
                                                    "\tLL >= 15.13, p < .0001";
        public string PluginTutorial { get; } = "Coming Soon";
        public bool TopLevel { get; } = true;
        public string StatusToReport { get; set; } = "";
        public Icon GetPluginIcon
        {
            get
            {
                return Properties.Resources.icon;
            }
        }

        #endregion




        private string SettingsFormQuotes = "\"";
        private string SettingsFormDelimiter = ",";
        private string SettingsFormEncoding = "utf-8";

        private List<CorpusProperties> CorporaFileDetails = new List<CorpusProperties>();
        private string OutputLocation = "";
        private string OutputFileEncoding = "utf-8";
        private string OutputFileDelimiter = ",";
        private string OutputFileQuote = "\"";
        private ulong CurrentNgramCount { get; set; } = 0;
        private bool omitZeroes { get; set; } = true;




        public void ChangeSettings()
        {

            using (var form = new SettingsForm_CompareFrequencies(OutputLocation, OutputFileEncoding, OutputFileQuote, OutputFileDelimiter, SettingsFormQuotes, SettingsFormDelimiter,
                                                                   SettingsFormEncoding, CorporaFileDetails, omitZeroes))
            {
                form.Icon = Properties.Resources.icon;
                form.Text = PluginName;

                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    OutputLocation = form.OutputFileLocation;
                    OutputFileEncoding = form.OutputFileSelectedEncoding;
                    OutputFileQuote = form.OutputFileQuote;
                    OutputFileDelimiter = form.OutputFileDelimiter;
                    SettingsFormQuotes = form.SettingsForm_Quote;
                    SettingsFormDelimiter = form.SettingsForm_Delimiter;
                    SettingsFormEncoding = form.SettingsForm_SelectedEncoding;
                    CorporaFileDetails = form.CorpPropList;
                    omitZeroes = form.omitZeroFromOutput;
                }
            }

        }





        //not used
        public Payload RunPlugin(Payload Input)
        {
            return new Payload();
        }









        public Payload RunPlugin(Payload Input, int ThreadsAvailable)
        {


            int NumCorpora = CorporaFileDetails.Count;
            int corporaPairedAtOnce = 2;
            ulong[] corporaSizes = new ulong[NumCorpora];
            for (int i = 0; i < CorporaFileDetails.Count; i++) corporaSizes[i] = CorporaFileDetails[i].Size;

            Dictionary<string, ulong[]> Frequencies = new Dictionary<string, ulong[]>();
            Dictionary<string, ulong[]> Ranks = new Dictionary<string, ulong[]>();

            int numComparisons = 1;
            if (NumCorpora > corporaPairedAtOnce) numComparisons = (int)((Factorial(NumCorpora) / Factorial(corporaPairedAtOnce)) * Factorial(NumCorpora - corporaPairedAtOnce));




            using (ThreadsafeOutputWriter OutputWriter = new ThreadsafeOutputWriter(OutputLocation, Encoding.GetEncoding(OutputFileEncoding.ToString()), FileMode.Create))
            {


                //write the header here
                #region Set up and write the header
                List<string> HeaderData = new List<string>();
                HeaderData.Add("ngram");
                foreach (CorpusProperties corpus in CorporaFileDetails) HeaderData.Add(corpus.Name + "_Rank");
                foreach (CorpusProperties corpus in CorporaFileDetails) HeaderData.Add(corpus.Name + "_Freq");
                

                for (int i = 0; i < CorporaFileDetails.Count; i++)
                {

                    for (int j = i + 1; j < CorporaFileDetails.Count; j++)
                    {

                        string Comparison = CorporaFileDetails[i].Name + " vs " + CorporaFileDetails[j].Name + ": ";

                        HeaderData.Add(Comparison + "LL");
                        HeaderData.Add(Comparison + "%DIFF");
                        HeaderData.Add(Comparison + "BIC");
                        HeaderData.Add(Comparison + "ELL");
                        HeaderData.Add(Comparison + "RRisk");
                        HeaderData.Add(Comparison + "LogRatio");
                        HeaderData.Add(Comparison + "OddsRatio");
                    }

                }

                //make sure that everything in the header data is wrapped in quotes, and that any quotes within the header item are escaped
                for (int i = 0; i < HeaderData.Count; i++) HeaderData[i] = OutputFileQuote + HeaderData[i].Replace(OutputFileQuote, OutputFileQuote + OutputFileQuote) + OutputFileQuote;

                string HeaderRow = String.Join(OutputFileDelimiter, HeaderData.ToArray());

                OutputWriter.WriteString(HeaderRow);
                #endregion


                #region Read in Each Frequency List
                //now we need to read in the data from each frequency list
                bool readError = false;
                
                for (int i = 0; i < CorporaFileDetails.Count; i++)
                {

                    //we just make a copy so that the code below is more legible
                    CorpusProperties corpus = CorporaFileDetails[i];
                    ulong CorpusSize = 0;

                    StatusToReport = "Loading Frequency List for " + corpus.Name + "...";


                    try
                    {
                        using (var stream = File.OpenRead(corpus.FileLocation))
                        using (var reader = new StreamReader(stream, encoding: Encoding.GetEncoding(corpus.FileEncoding)))
                        {
                            var data = CsvParser.ParseHeadAndTail(reader, corpus.Delimiter[0], corpus.Quote[0]);

                            var header = data.Item1;
                            var lines = data.Item2;

                            foreach (var line in lines)
                            {
                                ulong Rank = Convert.ToUInt64(line[1]);
                                string ngram = line[2];
                                ulong Freq = Convert.ToUInt64(line[3]);

                                //this is where we figure out the size of the corpus when reading it in here
                                if (Convert.ToInt32(line[7]) == 1) CorpusSize += Convert.ToUInt64(line[3]);

                                if (Frequencies.ContainsKey(ngram))
                                {
                                    Frequencies[ngram][i] = Freq;
                                }
                                else
                                {
                                    Frequencies.Add(ngram, new ulong[NumCorpora]);
                                    for (int j = 0; j < NumCorpora; j++) Frequencies[ngram][j] = 0;
                                    Frequencies[ngram][i] = Freq;
                                }

                                if (Ranks.ContainsKey(ngram))
                                {
                                    Ranks[ngram][i] = Rank;
                                }
                                else
                                {
                                    Ranks.Add(ngram, new ulong[NumCorpora]);
                                    for (int j = 0; j < NumCorpora; j++) Ranks[ngram][j] = 0;
                                    Ranks[ngram][i] = Rank;
                                }


                            }
                        }

                        //now that we've read in the corpus, we should manually set the corpus size again
                        //this is because, as it stands now, the corpus size is only calculated upon initially
                        //choosing the corpus in the settings window.
                        corporaSizes[i] = CorpusSize;


                    }
                    catch
                    {
                        MessageBox.Show("There was an error while trying to read your Frequency List file for \"" + corpus.Name + "\". If you currently have the Frequency List file open in another program, please close it and try again. This error can also be caused when your spreadsheet is not correctly formatted, or that your selections for delimiters and quotes are not the same as what is used in your spreadsheet.", "Error reading spreadsheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        readError = true;
                        break;
                    }

                    if (readError) break;




                }

                if (readError) return new Payload();
                #endregion






                //then we go on to calculating all of the scores and writing them to the output


                Int64 ngramsProcessed = 0;



                TimeSpan reportPeriod = TimeSpan.FromMinutes(0.01);
                using (new System.Threading.Timer(
                            _ => SetUpdate(ngramsProcessed),
                                 null, reportPeriod, reportPeriod))
                {
                    Parallel.ForEach((IEnumerable<string>)Frequencies.Keys,
                    new ParallelOptions { MaxDegreeOfParallelism = ThreadsAvailable }, (key, state) =>
                    {


                        ComparisonMetrics metrics = new ComparisonMetrics(key, Ranks[key], Frequencies[key], numComparisons, corporaSizes, omitZeroes);
                        string[] OutputArr = metrics.GetOutput();
                        string[] outputRow = new string[OutputArr.Length + 1];
                        outputRow[0] = key;
                        for (int i = 0; i < OutputArr.Length; i++) outputRow[i + 1] = OutputArr[i];
                        //make sure that quotes are doubled as escapes
                        for (int i = 0; i < outputRow.Length; i++) outputRow[i] = OutputFileQuote + outputRow[i].Replace(OutputFileQuote, OutputFileQuote + OutputFileQuote) + OutputFileQuote;
                        OutputWriter.WriteString(String.Join(OutputFileDelimiter, outputRow));

                        Interlocked.Increment(ref ngramsProcessed);


                    });
                }




                //end outputwriter
            }

            return new Payload();

        }








        public void Initialize() 
        {
            CurrentNgramCount = 0;
        }





        public bool InspectSettings()
        {
            if (CorporaFileDetails.Count >= 2)
            {

                if (!String.IsNullOrEmpty(OutputLocation))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                MessageBox.Show("You must select 2 or more frequency lists to compare for the \"Compare Frequencies\" plugin.", "Compare Frequencies Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


                
        }

        public Payload FinishUp(Payload Input)
        {
            StatusToReport = "Finished!";
            return (Input);
        }






        #region Import/Export Settings
        public void ImportSettings(Dictionary<string, string> SettingsDict)
        {
            SettingsFormQuotes = SettingsDict["SettingsFormQuotes"];
            SettingsFormDelimiter = SettingsDict["SettingsFormDelimiter"];
            SettingsFormEncoding = SettingsDict["SettingsFormEncoding"];
            OutputLocation = SettingsDict["OutputLocation"];
            OutputFileEncoding = SettingsDict["OutputFileEncoding"];
            OutputFileDelimiter = SettingsDict["OutputFileDelimiter"];
            OutputFileQuote = SettingsDict["OutputFileQuote"];
            int NumberOfCorpora = Convert.ToInt32(SettingsDict["NumberOfCorpora"]);

            omitZeroes = Boolean.Parse(SettingsDict["omitZeroes"]);

            CorporaFileDetails = new List<CorpusProperties>();

            for (int i = 0; i < NumberOfCorpora; i++)
            {
                string CorpusFileDetailsLeadingString = "CorpusFileDetails_" + i.ToString() + "_";
                string Corpus_Delimiter = SettingsDict[CorpusFileDetailsLeadingString + "Delimiter"];
                string Corpus_FileEncoding = SettingsDict[CorpusFileDetailsLeadingString + "FileEncoding"];
                string Corpus_FileLocation = SettingsDict[CorpusFileDetailsLeadingString + "FileLocation"];
                string Corpus_Name = SettingsDict[CorpusFileDetailsLeadingString + "Name"];
                string Corpus_Quote = SettingsDict[CorpusFileDetailsLeadingString + "Quote"];
                ulong Corpus_Size = Convert.ToUInt64(SettingsDict[CorpusFileDetailsLeadingString + "Size"]);

                CorpusProperties corpProp = new CorpusProperties(Corpus_FileLocation, Corpus_FileEncoding, Corpus_Quote, Corpus_Delimiter, Corpus_Name, Corpus_Size);
                CorporaFileDetails.Add(corpProp);

            }

        }




        public Dictionary<string, string> ExportSettings(bool suppressWarnings)
        {
            Dictionary<string, string> SettingsDict = new Dictionary<string, string>();
            SettingsDict["SettingsFormQuotes"] = SettingsFormQuotes;
            SettingsDict["SettingsFormDelimiter"] = SettingsFormDelimiter;
            SettingsDict["SettingsFormEncoding"] = SettingsFormEncoding;
            SettingsDict["OutputLocation"] = OutputLocation;
            SettingsDict["OutputFileEncoding"] = OutputFileEncoding;
            SettingsDict["OutputFileDelimiter"] = OutputFileDelimiter;
            SettingsDict["OutputFileQuote"] = OutputFileQuote;

            SettingsDict["NumberOfCorpora"] = CorporaFileDetails.Count.ToString();
            SettingsDict["omitZeroes"] = omitZeroes.ToString();


            for (int i = 0; i < CorporaFileDetails.Count; i++)
            {
                string CorpusFileDetailsLeadingString = "CorpusFileDetails_" + i.ToString() + "_";
                SettingsDict[CorpusFileDetailsLeadingString + "Delimiter"] = CorporaFileDetails[i].Delimiter;
                SettingsDict[CorpusFileDetailsLeadingString + "FileEncoding"] = CorporaFileDetails[i].FileEncoding;
                SettingsDict[CorpusFileDetailsLeadingString + "FileLocation"] = CorporaFileDetails[i].FileLocation;
                SettingsDict[CorpusFileDetailsLeadingString + "Name"] = CorporaFileDetails[i].Name;
                SettingsDict[CorpusFileDetailsLeadingString + "Quote"] = CorporaFileDetails[i].Quote;
                SettingsDict[CorpusFileDetailsLeadingString + "Size"] = CorporaFileDetails[i].Size.ToString();
                
            }


            return (SettingsDict);
        }
        #endregion


        
        



        private void SetUpdate(Int64 nGramCount)
        {
            StatusToReport = "Metrics have been calculated for " + nGramCount.ToString() + " n-grams...";
        }


        private decimal Factorial(int number)
        {
            decimal fact = number;
            for (int i = number - 1; i >= 1; i--) fact = fact * i;
            return fact;
        }




    }
}
