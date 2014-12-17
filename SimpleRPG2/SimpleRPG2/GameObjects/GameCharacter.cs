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

        private int _totalHP;
        public int totalHP
        {
            get
            {
                return _totalHP + CoreHelper.getEffectAmount(new Random(), activeEffects, StatType.HitPoints);
            }
            set
            {
                _totalHP = value;
            }
        }

        public int hp { get; set; }

        private int _attack;
        public int attack { get { return _attack + CoreHelper.getEffectAmount(new Random(), activeEffects, StatType.Attack); } set { _attack = value; } }

        public int ap { get; set; }
        public int totalAP { get; set; }

        public List<Item> inventory { get; set; }
        public Weapon weapon { get; set; }
        public Armor armor { get; set; }

        public List<ActiveEffect> activeEffects { get; set; }
        public List<PassiveEffect> passiveEffects { get; set; }

        public List<Ability> abilityList { get; set; }

        public GameCharacter() {
            inventory = new List<Item>();
            activeEffects = new List<ActiveEffect>();
            passiveEffects = new List<PassiveEffect>();
            abilityList = new List<Ability>();
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

        public void AddActiveEffect(ActiveEffect a, BattleGame game)
        {
            ActivateEffect(a,game);

            a.duration--;
            if(a.duration > 0)
            {
                activeEffects.Add(a);
            }
        }

        //Occurs once per turn
        public void RunActiveEffects(BattleGame game)
        {
            for (int i = activeEffects.Count - 1; i >= 0;i-- )
            {
                ActivateEffect(activeEffects[i],game);

                activeEffects[i].duration--;
                if(activeEffects[i].duration <=0)
                {
                    activeEffects.RemoveAt(i);
                }
            }
        }

        private void ActivateEffect(ActiveEffect effect, BattleGame game)
        {
            switch(effect.statType)
            {
                case StatType.Damage:
                    this.Damage(game.r.Next(effect.minAmount, effect.maxAmount),game);
                    break;
                case StatType.Heal:
                    this.Heal(game.r.Next(effect.minAmount, effect.maxAmount), game);
                    break;
                default:
                    break;
            }
        }

        public void Damage(int amount, BattleGame game)
        {

            game.battleLog.AddEntry(string.Format("{0} was hurt for {1}", this.name, amount));

            this.hp -= amount;
            if(this.hp < 0)
            {
                Kill(game);
            }

         
        }

        public void Heal(int amount, BattleGame game)
        {
            this.hp += amount;
            if(this.hp > this.totalHP)
            {
                this.hp = this.totalHP;
            }

            game.battleLog.AddEntry(string.Format("{0} was healed for {1}", this.name, amount));

        }

        public void Kill(BattleGame game)
        {
            game.CharacterKill(this);
        }


        public override string ToString()
        {
            string retval = name + "\n";
            retval += string.Format("AC: {0} HP: {1}/{2} Atk: {3} AP: {4}/{5}\n", ac, hp, totalHP, attack, ap, totalAP);

            retval += weapon.ToString() + "\n";
            foreach(var ae in activeEffects)
            {
                retval += ae.ToString() + "\n";
            }

            return retval;
        }
    }
}
