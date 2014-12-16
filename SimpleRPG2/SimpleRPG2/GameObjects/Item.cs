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

        public string getEffects()
        {
            string retval = "";
            foreach(var p in passiveEffects)
            {
                retval+= p.ToString() + " ";
            }

            foreach(var a in activeEffects)
            {
                retval += a.ToString() + " ";
            }

            return retval;
        }

    }

    public class UsableItem : Item
    {
        public int actionPoints { get; set; }
        public int uses { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ap: {1} use: {2} effects: {3}", name, actionPoints,uses, getEffects());
        }
    }

    public class Weapon : Item
    {
        public int damage { get; set; }
        public int actionPoints { get; set; }

        public override string ToString()
        {
            return string.Format("{0} dmg: {1} ap: {2}", name, damage,actionPoints);
        }

    }

    public class Armor : Item
    {
        public int armor { get; set; }
    }

   
    
}
