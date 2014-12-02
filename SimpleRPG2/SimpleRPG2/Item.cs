using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class Item
    {
        public string name { get; set; }
        public ItemType type { get; set; }
        public List<PassiveEffect> passiveEffects { get; set; }
        public List<ActiveEffect> activeEffects { get; set; }
    }

    public class Weapon : Item
    {
        public int damage { get; set; }
        public int actionPoints { get; set; }

    }

    public class Armor : Item
    {
        public int armor { get; set; }
    }

    public class PassiveEffect
    {
        public string name { get; set; }
        public StatType statType { get; set; }
        public int amount {get;set;}

    }

    public class ActiveEffect
    {
        public string name { get; set; }
        public StatType statType { get; set; }
        public int amount { get; set; }
        public int duration { get; set; }
    }
}
