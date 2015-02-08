using System.Collections.Generic;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.NPC
{
    using Base;
    class Enemy : Npc, IRoam, IEnemy
    {
        public Enemy(string id, string name, int health,
            int attack, int defense, List<Stat> otherStats)
            : base(id, name, health, defense)
        {
            this.AttackPower = new Attack(attack);
            this.Stats = otherStats;
        }

        public Enemy(string id, string name, int health,
            int attack, int defense)
            : this(id, name, health, attack, defense, null)
        {
        }

        public List<Stat> Stats { get; private set; }

        public Attack AttackPower { get; private set; }

        public void Attack(Npc target)
        {
            int damage = this.AttackPower.Value; //Add more values to formula as more stat types get implemented.
            target.TakeDamage(damage);
        }

        public void Roam()
        {
            throw new System.NotImplementedException();
        }

        public override void Modify(Stat stat, bool remove = false)
        {
            
            if (remove == true)
            {
                #region RemoveStats
                if (stat.GetType() == typeof (Attack))
                {
                    this.AttackPower.Value -= stat.Value;
                }
                else if (stat.GetType() == typeof (Defense))
                {
                    this.Defense.Value -= stat.Value;
                }
                else
                {
                    this.Stats.Find(entry => entry == stat).Value -= stat.Value;
                }
                #endregion
            }
            else
            {
                #region AddStats
                if (stat.GetType() == typeof(Attack))
                {
                    this.AttackPower.Value += stat.Value;
                }
                else if (stat.GetType() == typeof(Defense))
                {
                    this.Defense.Value += stat.Value;
                }
                else
                {
                    this.Stats.Find(entry => entry == stat).Value -= stat.Value;
                }
                #endregion
            }
        }
    }
}
