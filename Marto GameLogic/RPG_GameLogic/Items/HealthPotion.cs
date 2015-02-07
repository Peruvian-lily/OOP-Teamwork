using System;
using RPG_GameLogic.Interfaces;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Enums;
using RPG_GameLogic.Factories;

namespace RPG_GameLogic.Items
{
    public class HealthPotion : Consumable, IPotion
    {
        private int healAmount;

        public HealthPotion(StatsList statsList, Currency price, int healAmount,int id)
            : base(statsList, price, id)
        {
            this.HealAmount = healAmount;
        }
        public HealthPotion(StatsList statsList, Currency price, int healAmount)
            : this(statsList, price, healAmount, 0)
        {   
        }
        public HealthPotion()
            : this(new StatsList(), new Currency(), 0)
        {
        }

        public int HealAmount 
        {
            get { return this.healAmount; } 
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Healing amount can't be negative.");
                }

                this.healAmount = value;
            }
        }

        public bool isPermanent { get; set; }

        public StatsList Effect()
        {
            StatsList stats = ItemFactory.CreateStatsListWithDefaultValues();
            stats.BodyStats.HP = this.healAmount;
            return stats;
        }
    }
}
