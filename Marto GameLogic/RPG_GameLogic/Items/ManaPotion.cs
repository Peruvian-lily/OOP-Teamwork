using System;
using RPG_GameLogic.Interfaces;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Enums;
using RPG_GameLogic.Factories;

namespace RPG_GameLogic.Items
{
    public class ManaPotion : Consumable, IPotion
    {
        private int manaRefillAmount;

        public ManaPotion(StatsList statsList, Currency price, int manaRefillAmount,int id)
            : base(statsList, price, id)
        {
            this.ManaRefillAmount = manaRefillAmount;
        }
        public ManaPotion(StatsList statsList, Currency price, int manaRefillAmount)
            : this(statsList, price, manaRefillAmount, 0)
        {   
        }
        public ManaPotion()
            : this(new StatsList(), new Currency(), 0, 0)
        {
        }

        public int ManaRefillAmount 
        {
            get { return this.manaRefillAmount; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Mana refill amount can't be negative.");
                }

                this.manaRefillAmount = value;
            }
        }

        public bool isPermanent { get; set; }

        public StatsList Effect() 
        {
            StatsList stats = ItemFactory.CreateStatsListWithDefaultValues();
            stats.BodyStats.HP = this.ManaRefillAmount;
            return stats;
        }
    }
}
