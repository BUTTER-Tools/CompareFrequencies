using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareFrequencies
{
    class ComparisonMetrics
    {

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


        public ComparisonMetrics(string ngram, ulong[] rankarray, ulong[] freqarray, int numComparisons, ulong[] corporaSizeArr)
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

            

            this.Calculate(ngram, freqs.Length);
        }



        private void Calculate(string ngram, int numCorpora)
        {
            
            int comparisonNumber = 0;

            for (int i = 0; i < numCorpora; i++)
            {

                if (freqs[i] == 0 || corporaSizes[i] == 0) continue;

                for (int j = i + 1; j < numCorpora; j++)
                {

                    if (freqs[j] ==0 || corporaSizes[j] == 0) continue;

                    decimal expectedFreq_1 = corporaSizes[i] * (freqs[i] + freqs[j]) / (corporaSizes[i] + corporaSizes[j]);
                    decimal expectedFreq_2 = corporaSizes[j] * (freqs[i] + freqs[j]) / (corporaSizes[i] + corporaSizes[j]);

                    decimal smallerExpectedFreq = 0;
                    if (expectedFreq_1 < expectedFreq_2)
                    {
                        smallerExpectedFreq = expectedFreq_1;
                    }
                    else
                    {
                        smallerExpectedFreq = expectedFreq_2;
                    }
                    
                    LL[comparisonNumber] = 2 * ((freqs[i] * (decimal)Math.Log((double)(freqs[i] / expectedFreq_1))) + (freqs[j] * (decimal)Math.Log((double)(freqs[j] / expectedFreq_2))));
                    DIFF[comparisonNumber] = (((freqs[i] / corporaSizes[i]) - (freqs[j] / corporaSizes[j])) * 100) / (freqs[j] / corporaSizes[j]);
                    BIC[comparisonNumber] = LL[comparisonNumber] - (decimal)(1 * Math.Log(corporaSizes[i] + corporaSizes[j]));
                    ELL[comparisonNumber] = LL[comparisonNumber] / (decimal)((corporaSizes[i] + corporaSizes[j]) * Math.Log((double)smallerExpectedFreq));
                    RR[comparisonNumber] = (freqs[i] / corporaSizes[i]) / (freqs[j] / corporaSizes[j]);
                    OR[comparisonNumber] = (decimal)((freqs[i] / (corporaSizes[i] - freqs[i])) / (freqs[j] / (corporaSizes[j] - freqs[j])));

                    decimal LR_numerator = 0.5m;
                    decimal LR_denominator = 0.5m;
                    if (freqs[i] > 0 && corporaSizes[i] > 0) LR_numerator = freqs[i] / corporaSizes[i];
                    if (freqs[j] > 0 && corporaSizes[j] > 0) LR_denominator = freqs[j] / corporaSizes[j];

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
