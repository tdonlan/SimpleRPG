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


        public static int displayMenuGetInt(List<string> menu)
        {
            bool valid = false;
            int retval = -1;
            while (!valid)
            {
                foreach (var s in menu)
                {
                    Console.Write(s + "\n");
                }
                Console.Write(">");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid Input");
                }
                else if (Int32.TryParse(input, out retval))
                {
                    if(retval <=0 || retval > menu.Count)
                    {
                        Console.WriteLine("Invalid Input");
                    }
                    else { valid = true; }
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            return retval;
        }

        //inclue a regex or a list of accepted values?
        public static string displayMenuGetStr(List<string> menu)
        {
            bool valid = false;
            string input = "";
            while (!valid)
            {
                foreach (var s in menu)
                {
                    Console.Write(s + "\n");
                }
                Console.Write(">");
                input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid Input");
                }
                {
                    valid = true;
                }
            }
            return input;
        }
    }
}
