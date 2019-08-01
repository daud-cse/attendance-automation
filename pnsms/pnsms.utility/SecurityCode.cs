using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pnsms.utility
{
    public class SecurityCode
    {
        public static string RandomNumber(int length)
        {
            Random rnd = new Random();
            int min = (int)Math.Pow(10, length - 1);
            int max = (int)Math.Pow(10, length) - 1;
            return rnd.Next(min, max).ToString();
        }
    }
}
