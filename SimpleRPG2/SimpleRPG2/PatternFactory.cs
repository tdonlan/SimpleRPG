using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class PatternFactory
    {
        public static List<Point> getFourAdj()
        {
            return new List<Point>() { 
                new Point(-1,0),
                new Point(1,0),
                new Point(0,-1),
                new Point(0,1)
            };
        }
    }
}
