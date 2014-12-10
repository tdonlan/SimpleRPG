using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class AbilityFactory
    {
        public static Ability getFireball()
        {
            ActiveEffect fireballEffect = new ActiveEffect() { name = "Fireball", duration = 1, amount = -20, statType = StatType.HitPoints };

            Ability fireball = new Ability()
            {
                name = "Fireball",
                description = "Send a large ball of flame into a crowd of foes",
                ap = 5,
                range = 20,
                uses = 5,
                targetType = AbilityTargetType.LOSAOE,
                tilePatternType = TilePatternType.NineSquare,
                activeEffects = new List<ActiveEffect>() {fireballEffect },
                passiveEffects=null
            };

            return fireball;
        }
    }
}
