using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pnsms.utility
{
    public static class MobileNumber
    {
        public static string Rectify(string number)
        {
            if(number==null || number=="")
            {
                return "";
            }

            number = Regex.Replace(number, @"[^\d]", "");
            int length = number.Length;

            if (length>=10)
            {
                number = number.Substring(length - 10, 10);
                return "880" + number;
            }


            return "";
        }
    }
}
