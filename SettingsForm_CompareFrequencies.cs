using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System;





namespace CompareFrequencies
{
    internal partial class SettingsForm_CompareFrequencies : Form
    {


        #region Get and Set Options

        public string OutputFileLocation { get; set; }
        public string SettingsForm_SelectedEncoding { get; set; }
        public string SettingsForm_Delimiter { get; set; }
        public string SettingsForm_Quote { get; set; }
        public string OutputFileSelectedEncoding { get; set; }
        public string OutputFileQuote { get; set; }
        public string OutputFileDelimiter { get; set; }
        public List<CorpusProperties> CorpPropList { get; set; }
        #endregion



        public SettingsForm_CompareFrequencies(string OutputFileLoc, string OutputFileEncode, string OutputFileQuoteChar, string OutputFileDelimitChar,
                                                string SettingFormQuote, string SettingFormDelimiter, string SettingFormEncode,
                                                List<CorpusProperties> CorpProp)
        {
            InitializeComponent();

            foreach (var encoding in Encoding.GetEncodings())
            {
                EncodingDropdown.Items.Add(encoding.Name);
                OutputFileEncodingDropdown.Items.Add(encoding.Name);
            }

            try
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact(SettingFormEncode);
            }
            catch
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact(Encoding.Default.BodyName);
            }

            try
            {
                OutputFileEncodingDropdown.SelectedIndex = OutputFileEncodingDropdown.FindStringExact(SettingFormEncode);
            }
            catch
            {
                OutputFileEncodingDropdown.SelectedIndex = OutputFileEncodingDropdown.FindStringExact(Encoding.Default.BodyName);
            }




            CSVDelimiterTextbox.Text = SettingFormDelimiter;
            CSVQuoteTextbox.Text = SettingFormQuote;

            OutputFileDelimiterTextbox.Text = OutputFileDelimitChar;
            OutputFileQuoteTextbox.Text = OutputFileQuoteChar;
            OutputFileTextbox.Text = OutputFileLoc;

            this.SettingsForm_SelectedEncoding = SettingsForm_SelectedEncoding;


            CorpPropList = CorpProp;

            foreach (CorpusProperties item in CorpProp)
            {
                IncludedCorporaListBox.Items.Add(item.Name);
            }



        }












        private void SetOutputButton_Click(object sender, System.EventArgs e)
        {


            OutputFileTextbox.Text = "";

            if (CSVDelimiterTextbox.TextLength < 1 || CSVQuoteTextbox.TextLength < 1)
            {
                MessageBox.Show("You must enter characters for your delimiter and quotes, respectively. This plugin does not know how to read a delimited spreadsheet without this information.", "I need details for your spreadsheet!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Please choose the output location for your CSV file";
                dialog.FileName = "BUTTER-Frequency_Comparison.csv";
                dialog.Filter = "Comma-Separated Values (CSV) File (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (File.Exists(dialog.FileName.ToString()))
                        {
                            if (DialogResult.Yes == MessageBox.Show("BUTTER is about to overwrite your selected file. Are you ABSOLUTELY sure that you want to do this? All data currently contained in the selected file will immediately be deleted if you select \"Yes\".", "Overwrite File?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            {
                                using (var myFile = File.Create(dialog.FileName.ToString())) { }
                                OutputFileTextbox.Text = dialog.FileName.ToString();
                            }
                            else
                            {
                                OutputFileTextbox.Text = "";
                            }
                        }
                        else
                        {
                            using (var myFile = File.Create(dialog.FileName.ToString())) { }
                            OutputFileTextbox.Text = dialog.FileName.ToString();
                        }



                    }
                    catch
                    {
                        MessageBox.Show("BUTTER does not appear to be able to create this output file. Do you have write permissions for this file? Is the file already open in another program?", "Cannot Create File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        OutputFileTextbox.Text = "";
                    }
                }
            }




        }
















        private void OKButton_Click(object sender, System.EventArgs e)
        {
            this.SettingsForm_SelectedEncoding = EncodingDropdown.SelectedItem.ToString();
            this.OutputFileLocation = OutputFileTextbox.Text;
            this.OutputFileSelectedEncoding = OutputFileEncodingDropdown.SelectedItem.ToString();

            if (CSVQuoteTextbox.Text.Length > 0)
            {
                this.SettingsForm_Quote = CSVQuoteTextbox.Text;
            }
            else
            {
                this.SettingsForm_Quote = "\"";
            }
            if (CSVDelimiterTextbox.Text.Length > 0)
            {
                this.SettingsForm_Delimiter = CSVDelimiterTextbox.Text;
            }
            else
            {
                this.SettingsForm_Delimiter = ",";
            }

            if (OutputFileQuoteTextbox.Text.Length > 0)
            {
                this.OutputFileQuote = OutputFileQuoteTextbox.Text;
            }
            else
            {
                this.OutputFileQuote = "\"";
            }
            if (OutputFileDelimiterTextbox.Text.Length > 0)
            {
                this.OutputFileDelimiter = OutputFileDelimiterTextbox.Text;
            }
            else
            {
                this.OutputFileDelimiter = ",";
            }


            this.DialogResult = DialogResult.OK;

        }








        private void GetInputFileButton_Click(object sender, System.EventArgs e)
        {

            if (CSVDelimiterTextbox.TextLength < 1 || CSVQuoteTextbox.TextLength < 1)
            {
                MessageBox.Show("You must enter characters for your delimiter and quotes, respectively. This plugin does not know how to read a delimited spreadsheet without this information.", "I need details for your spreadsheet!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CorpusNameTextbox.TextLength < 1)
            {
                MessageBox.Show("You must enter a name for your corpus/frequency list before loading it.", "I need details!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var dialog = new OpenFileDialog())
            {

                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.ValidateNames = true;
                dialog.Title = "Please choose the BUTTER Frequency file that you would like to read";
                dialog.FileName = "BUTTER-FrequencyList.csv";
                dialog.Filter = "Comma-Separated Values (CSV) File (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    //this probably has to be a dictionary
                    //to track total frequencies across different n-gram sizes
                    ulong CorpusSize = 0;

                    string SelectedEncoding = EncodingDropdown.SelectedItem.ToString();

                    try
                    {
                        using (var stream = File.OpenRead(dialog.FileName))
                        using (var reader = new StreamReader(stream, encoding: Encoding.GetEncoding(SelectedEncoding)))
                        {
                            var data = CsvParser.ParseHeadAndTail(reader, CSVDelimiterTextbox.Text[0], CSVQuoteTextbox.Text[0]);

                            var header = data.Item1;
                            var lines = data.Item2;

                            foreach (var line in lines)
                            {
                                //if the row that we're looking at is a unigram, then add the frequency to CorpusSize
                                if (Convert.ToInt32(line[7]) == 1) CorpusSize += Convert.ToUInt64(line[3]);
                            }
                        }

                        CorpPropList.Add(new CorpusProperties(dialog.FileName, SelectedEncoding, CSVQuoteTextbox.Text[0].ToString(), CSVDelimiterTextbox.Text[0].ToString(), CorpusNameTextbox.Text, CorpusSize));
                        IncludedCorporaListBox.Items.Add(CorpusNameTextbox.Text);
                        CorpusNameTextbox.Text = "";
                        

                    }
                    catch
                    {
                        MessageBox.Show("There was an error while trying to read your BUTTER Frequency List file. If you currently have the Frequency List file open in another program, please close it and try again. This error can also be caused when your spreadsheet is not correctly formatted, or that your selections for delimiters and quotes are not the same as what is used in your spreadsheet.", "Error reading spreadsheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                }



            }



        }

        private void RemoveCorpusButton_Click(object sender, EventArgs e)
        {
            if (IncludedCorporaListBox.SelectedItems.Count > 0)
            {
                CorpPropList.RemoveAt(IncludedCorporaListBox.SelectedIndex);
                IncludedCorporaListBox.Items.RemoveAt(IncludedCorporaListBox.SelectedIndex);
            }
        }



    }
}
