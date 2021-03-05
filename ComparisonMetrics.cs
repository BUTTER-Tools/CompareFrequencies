using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareFrequencies
{
    class ComparisonMetrics
    {

        private const decimal zeroReplace = (decimal)0.5; //using a replacement of 0.5 for the Frequency to be consistent with the UCREL page

        private decimal[] freqs;
        private decimal[] ranks;
        private ulong[] corporaSizes;
        public decimal[] LL { get; }
        public decimal[] DIFF { get; }
        public decimal[] BIC { get; }
        public decimal[] ELL { get; }
        public decimal[] RR { get; }
        public decimal[] LR { get; }
        public decimal[] OR { get; }


        public ComparisonMetrics(string ngram, ulong[] rankarray, ulong[] freqarray, int numComparisons, ulong[] corporaSizeArr, bool omitZeroes)
        {
            freqs = new decimal[freqarray.Length];
            ranks = new decimal[rankarray.Length];
            //corporaSizes = new decimal[corporaSizeArr.Length];

            for (int i = 0; i < freqarray.Length; i++) freqs[i] = (decimal)freqarray[i];
            for (int i = 0; i < rankarray.Length; i++) ranks[i] = (decimal)rankarray[i];
            //for (int i = 0; i < corporaSizeArr.Length; i++) corporaSizes[i] = (decimal)corporaSizeArr[i];
            corporaSizes = corporaSizeArr;



            LL = new decimal[numComparisons];
            DIFF = new decimal[numComparisons];
            BIC = new decimal[numComparisons];
            ELL = new decimal[numComparisons];
            RR = new decimal[numComparisons];
            LR = new decimal[numComparisons];
            OR = new decimal[numComparisons];

            for (int i = 0; i < numComparisons; i++)
            {
                LL[i] = decimal.MinValue;
                DIFF[i] = decimal.MinValue;
                BIC[i] = decimal.MinValue;
                ELL[i] = decimal.MinValue;
                RR[i] = decimal.MinValue;
                LR[i] = decimal.MinValue;
                OR[i] = decimal.MinValue;
            }

            

            this.Calculate(ngram, freqs.Length, omitZeroes);
        }



        private void Calculate(string ngram, int numCorpora, bool omitZeroes)
        {
            
            int comparisonNumber = 0;

            for (int i = 0; i < numCorpora; i++)
            {

                if (corporaSizes[i] == 0) continue;

                for (int j = i + 1; j < numCorpora; j++)
                {

                    if (corporaSizes[j] == 0) continue;



                    decimal observedFreq_1;
                    decimal observedFreq_2;

                    if (freqs[i] != 0)
                    {
                        observedFreq_1 = freqs[i];
                    }
                    else
                    {
                        if (omitZeroes) continue;
                        observedFreq_1 = zeroReplace;
                    }
                    if (freqs[j] != 0)
                    {
                        observedFreq_2 = freqs[j];
                    }
                    else
                    {
                        if (omitZeroes) continue;
                        observedFreq_2 = zeroReplace;
                    }


                    decimal expectedFreq_1 = corporaSizes[i] * (observedFreq_1 + observedFreq_2) / (corporaSizes[i] + corporaSizes[j]);
                    decimal expectedFreq_2 = corporaSizes[j] * (observedFreq_1 + observedFreq_2) / (corporaSizes[i] + corporaSizes[j]);

                    decimal smallerExpectedFreq = 0;
                    if (expectedFreq_1 < expectedFreq_2)
                    {
                        smallerExpectedFreq = expectedFreq_1;
                    }
                    else
                    {
                        smallerExpectedFreq = expectedFreq_2;
                    }

                    LL[comparisonNumber] = 2 * ((observedFreq_1 * (decimal)Math.Log((double)(observedFreq_1 / expectedFreq_1))) + (observedFreq_2 * (decimal)Math.Log((double)(observedFreq_2 / expectedFreq_2))));
                    DIFF[comparisonNumber] = (((observedFreq_1 / corporaSizes[i]) - (observedFreq_2 / corporaSizes[j])) * 100) / (observedFreq_2 / corporaSizes[j]);
                    BIC[comparisonNumber] = LL[comparisonNumber] - (decimal)(1 * Math.Log(corporaSizes[i] + corporaSizes[j]));
                    ELL[comparisonNumber] = LL[comparisonNumber] / (decimal)((corporaSizes[i] + corporaSizes[j]) * Math.Log((double)smallerExpectedFreq));
                    RR[comparisonNumber] = (observedFreq_1 / corporaSizes[i]) / (observedFreq_2 / corporaSizes[j]);
                    OR[comparisonNumber] = (decimal)((observedFreq_1 / (corporaSizes[i] - observedFreq_1)) / (observedFreq_2 / (corporaSizes[j] - observedFreq_2)));

                    decimal LR_numerator = 0.5m;
                    decimal LR_denominator = 0.5m;

                    LR_numerator = observedFreq_1 / corporaSizes[i];
                    LR_denominator = observedFreq_2 / corporaSizes[j];
                    LR[comparisonNumber] = (decimal)Math.Log((double)LR_numerator / (double)LR_denominator, 2);

                    comparisonNumber++;

                }
            }


        }


        public string[] GetOutput()
        {
            string[] output = new string[ranks.Length + freqs.Length + LL.Length + DIFF.Length + BIC.Length + ELL.Length + RR.Length + LR.Length + OR.Length];
            for (int i = 0; i < output.Length; i++) output[i] = "";


            for (int i = 0; i < ranks.Length; i++) output[i] = ranks[i].ToString();
            for (int i = 0; i < freqs.Length; i++) output[i + ranks.Length] = freqs[i].ToString();

            int comparisonNumber = 0;
            int outputIndex = 0 + ranks.Length + freqs.Length;

            for (int i = 0; i < freqs.Length; i++)
            {
                for (int j = i + 1; j < freqs.Length; j++)
                {

                    if (LL[comparisonNumber] != decimal.MinValue) output[outputIndex] = LL[comparisonNumber].ToString();
                    outputIndex++;
                    if (DIFF[comparisonNumber] != decimal.MinValue) output[outputIndex] = DIFF[comparisonNumber].ToString();
                    outputIndex++;
                    if (BIC[comparisonNumber] != decimal.MinValue) output[outputIndex] = BIC[comparisonNumber].ToString();
                    outputIndex++;
                    if (ELL[comparisonNumber] != decimal.MinValue) output[outputIndex] = ELL[comparisonNumber].ToString();
                    outputIndex++;
                    if (RR[comparisonNumber] != decimal.MinValue) output[outputIndex] = RR[comparisonNumber].ToString();
                    outputIndex++;
                    if (LR[comparisonNumber] != decimal.MinValue) output[outputIndex] = LR[comparisonNumber].ToString();
                    outputIndex++;
                    if (OR[comparisonNumber] != decimal.MinValue) output[outputIndex] = OR[comparisonNumber].ToString();
                    outputIndex++;

                    comparisonNumber++;

                }
            }


            return output;
        }





    }



}
