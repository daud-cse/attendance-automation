using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.utility
{
    public static class CheckDigit
    {
        // Multiplicative Table
        static int[,]
        M = { {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
              {1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
              {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
              {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
              {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
              {5, 9, 8, 7, 6, 0, 4, 3, 2 ,1},
              {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
              {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
              {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
              {9, 8, 7, 6, 5, 4, 3, 2, 1, 0} };

        static int[] inv = { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };
        static int[][] D = new int[8][];

        public static string Decode(string txt)
        {
            Initializer();

            string txtS_;
            string txtS = "";

            txtS_ = txt.Substring(0, 1);
            txt = txt.Substring(1, (txt.Length - 1));
            txtS = Encription(txt);

            if (txtS_ == txtS) return txt;
            else return "Invalid";
        }

        public static string Encode(string txt)
        {
            Initializer();
            string txtS = "";
            txtS = Encription(txt);
            return txtS + txt;
        }

        static string Encription(string txt)
        {
            string[] reversedInput = new string[txt.Length];
            for (int i = 0; i < txt.Length; i++)
            {
                reversedInput[i] = txt[txt.Length - (i + 1)].ToString();
            }

            int check = 0, d;
            for (int i = 0; i < reversedInput.Length; i++)
            {
                if (reversedInput[i] == "0" || reversedInput[i] == "1" || reversedInput[i] == "2" || reversedInput[i] == "3"
                    || reversedInput[i] == "4" || reversedInput[i] == "5" || reversedInput[i] == "6" || reversedInput[i] == "7"
                    || reversedInput[i] == "8" || reversedInput[i] == "9")
                {
                    int ss = Convert.ToInt16(reversedInput[i]);
                    int pp = (i + 1) % 8;
                    d = D[pp][ss];
                    check = M[check, d];
                }
            }
            int checkDigit = inv[check];
            return checkDigit.ToString();
        }        
        static void Initializer()
        {
            D[0] = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };  // identity permutation
            D[1] = new int[] { 1, 5, 7, 6, 2, 8, 3, 0, 9, 4 };  // "magic" permutation

            for (int i = 2; i < 8; i++)
            {
                // iterate for remaining permutations
                D[i] = new int[10];
                for (int j = 0; j < 10; j++)
                {
                    D[i][j] = D[i - 1][D[1][j]];
                }
            }
        }
    }
}
