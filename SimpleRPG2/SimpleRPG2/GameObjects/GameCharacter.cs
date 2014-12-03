using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class GameCharacter
    {
        public string name { get; set; }
        public char displayChar { get; set; }
        public CharacterType type { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public int ac { get; set; }
        public int totalHP { get; set; }
        public int hp { get; set; }
        public int attack { get; set; }
        public int ap { get; set; }
        public int totalAP { get; set; }

        public List<Item> inventory { get; set; }
        public Weapon weapon { get; set; }
        public Armor armor { get; set; }

        public GameCharacter() { }

        public override string ToString()
        {
            string retval = name + "\n";
            retval += string.Format("AC: {0} HP: {1}/{2} Atk: {3} AP: {4}/{5}\n", ac, hp, totalHP, attack, ap,totalAP);
            retval += weapon.ToString() + "\n";

            return retval;
        }

        public bool SpendAP(int ap)
        {
            if(this.ap >= ap)
            {
                this.ap -= ap;
                return true;
            }
            return false;
        }

        public void ResetAP()
        {
            this.ap = totalAP;
        }


    }
}
