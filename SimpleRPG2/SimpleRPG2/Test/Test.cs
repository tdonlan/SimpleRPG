using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class Test
    {
        public static void InputTest()
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine("Enter Name");
                Console.Write(">");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Please enter a name");
                }
                else
                {
                    Console.WriteLine("Hello " + name + "!");
                    done = true;
                }
                Console.WriteLine("------");
            }
        }

        public static void testMenu()
        {
            List<string> menu = new List<string>() { "1. View", "2. Move", "3. Attack" };
            int input = getMenuInput(menu);
            Console.WriteLine("Input was " + input.ToString());
        }

        public static int getMenuInput(List<string> menu)
        {
            bool valid = false;
            int retval = -1;
            while(!valid)
            {
                foreach(var s in menu)
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
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            return retval;
        }
    }
}
