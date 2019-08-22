using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace pnsms.utility
{
    public static class NumericFormat
    {
        static string[] Steps { get { return new string[] { "", "Hundred ", "Thousand ", "Lak " }; } }
        static int[] StepSize { get { return new int[] { 100, 1000, 100000, 10000000 }; } }
        static int ChunkSize { get { return 10000000; } }
        static string ChunkValue { get { return "Crore "; } }

        public static string InWords(int amount)
        {
            return GetWords((double)amount);
        }

        public static string InWords(double amount)
        {
            return GetWords(amount);
        }

        public static string InWords(float amount)
        {
            return GetWords((float)amount);
        }

        static string GetWords(double amount)
        {
            string result = "", paisha = "";
            double tempAmount, amount_;
            bool enableCrore = false;

            string psh, dec, str = amount.ToString();
            int index = str.IndexOf('.');

            if (index > 0)
            {
                str = str + "0";
                psh = str.Substring(index + 1, 2);
                dec = str.Substring(0, index);
                amount_ = Convert.ToDouble(dec);

                paisha = GetTenthInWords(Convert.ToInt32(psh)) + "Paisha";
            }
            else
            {
                amount_ = amount;
            }

            while (amount_ > 0)
            {
                tempAmount = (amount_ % ChunkSize);

                result = GetChunkValue(Convert.ToInt32(tempAmount)) + (enableCrore ? ChunkValue : "") + result;

                amount_ = (amount_ - tempAmount) / ChunkSize;

                enableCrore = true;
            }

            result = result == "" ? "" : result + "Taka ";

            return result + paisha;
        }
        static string GetChunkValue(int value)
        {
            string result = "";
            int tempValue, previousStepSize;
            int index = 0;
            while (value > 0)
            {
                tempValue = (value % StepSize[index]);
                previousStepSize = index == 0 ? 1 : StepSize[index - 1];

                if (tempValue > 0)
                {
                    result = GetTenthInWords(tempValue / previousStepSize) + Steps[index] + result;
                }

                value = value - tempValue;

                index++;
            }

            return result;
        }
        static string GetTenthInWords(int value)
        {
            if (value >= 100) return "";
            else if (value < 20)
            {
                return Get1to19(value) + " ";
            }
            else
            {
                int tmp1 = value / 10;
                int tmp2 = value % 10;

                return Get20to99(tmp1) + " " + Get1to19(tmp2) + " ";
            }
        }
        static string Get1to19(int value)
        {
            switch (value)
            {
                case 0:
                    return "";
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                case 7:
                    return "Seven";
                case 8:
                    return "Eight";
                case 9:
                    return "Nine";
                case 10:
                    return "Ten";
                case 11:
                    return "Eleven";
                case 12:
                    return "Twelve";
                case 13:
                    return "Thirteen";
                case 14:
                    return "Fourteen";
                case 15:
                    return "Fifteen";
                case 16:
                    return "Sixteen";
                case 17:
                    return "Seventeen";
                case 18:
                    return "Eighteen";
                case 19:
                    return "Nineteen";
                default:
                    return "";
            }
        }
        static string Get20to99(int value)
        {
            switch (value)
            {
                case 2:
                    return "Twenty";
                case 3:
                    return "Thirty";
                case 4:
                    return "Forty";
                case 5:
                    return "Fifty";
                case 6:
                    return "Sixty";
                case 7:
                    return "Seventy";
                case 8:
                    return "Eighty";
                case 9:
                    return "Ninety";
                default:
                    return "";
            }
        }
    }
}
