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

        public static Ability getHeal()
        {
            ActiveEffect healEffect = new ActiveEffect() { name = "Heal", duration = 1, amount = 10, statType = StatType.HitPoints };
            Ability heal = new Ability()
            {
                name = "Heal",
                description = "Heal Self",
                ap = 5,
                range = 1,
                uses = 1,
                targetType = AbilityTargetType.Self,
                tilePatternType = TilePatternType.Single,
                activeEffects = new List<ActiveEffect>() { healEffect },
                passiveEffects = null
            };

            return heal;
        }

        public static Ability getTeleport()
        {
            ActiveEffect teleportEffect = new ActiveEffect() { name = "Teleport", duration = 1, amount = 0, statType = StatType.MoveSelf };
            Ability teleport = new Ability()
            {
                name = "Teleport",
                description = "Teleport to a selected location on the map",
                ap = 10,
                range = 20,
                uses = 1,
                targetType = AbilityTargetType.PointEmpty,
                tilePatternType = TilePatternType.Single,
                activeEffects = new List<ActiveEffect>() { teleportEffect},
                passiveEffects = null
            };

            return teleport;
        }

        public static Ability getKnockback()
        {
            ActiveEffect knockbackEffect = new ActiveEffect() { name = "Knockback", duration = 1, amount = 1, statType = StatType.MoveTarget };
            Ability knockback = new Ability()
            {
                name = "Knockback",
                description = "Knockback the target 1 tile",
                ap = 5,
                range = 1,
                uses = 1,
                targetType = AbilityTargetType.SingleFoe,
                tilePatternType = TilePatternType.Single,
                activeEffects = new List<ActiveEffect>() { knockbackEffect },
                passiveEffects = null,

            };

            return knockback;


        }
    }
}
