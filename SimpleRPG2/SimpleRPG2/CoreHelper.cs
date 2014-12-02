using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class CoreHelper
    {
        //returns a list of characters A, B, C, etc to use when displaying board
        public static List<char> getLetterList()
        {
            return new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        }

        //2 digits
        public static string getPaddedNum(int num)
        {
            if(num <10)
            {
                return " " + num.ToString();
            }
            else
            {
                return num.ToString();
            }
        }
    }
}
