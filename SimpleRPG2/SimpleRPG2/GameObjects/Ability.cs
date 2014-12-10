using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class Ability
    {
        public string name { get; set; }
        public string description { get; set; }
        public int ap { get; set; }
        public int uses { get; set; }

        public int range { get; set; }

        public AbilityTargetType targetType { get; set; }
        public TilePatternType tilePatternType { get; set; } //only used for AOE abilities

        public List<ActiveEffect> activeEffects { get; set; }
        public List<PassiveEffect> passiveEffects { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: ap:{1} uses:{2} | {3}", name, ap, uses, description);
        }

        public bool spendUses(int uses)
        {
            if(this.uses >= uses)
            {
                this.uses -= uses;
                return true;
            }
            return false;
        }
    }
}
