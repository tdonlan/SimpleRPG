using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class PassiveEffect
    {
        public string name { get; set; }
        public StatType statType { get; set; }
        public int amount { get; set; }

        public override string ToString()
        {
            return string.Format("{0} (Passive): {1} {2}", name, statType.ToString(), amount);
        }

    }

    public class ActiveEffect
    {
        public string name { get; set; }
        public StatType statType { get; set; }
        public int amount { get; set; }
        public int duration { get; set; }

        public override string ToString()
        {
            return string.Format("{0} (Active): {1} {2} {3} turns", name, statType.ToString(), amount, duration);
        }
    }

}
