using System;
using RPG_GameLogic.Interfaces;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Enums;

namespace RPG_GameLogic.Items
{
    public abstract class Item : GameObject, IStatistics
    {
        private StatsList stats;
        private Currency sellPrice;
        private ItemQuality itemQuality;

        public Item(StatsList stats, Currency price, ItemQuality itemQuality, int id)
            : base(id) 
        {
            this.Stats = stats;
            this.SellPrice = sellPrice;
            this.ItemQuality = itemQuality;
        }
        public Item(StatsList stats, Currency price, ItemQuality itemQuality)
            : this(stats, price, itemQuality, 0)
        {
        }
        public Item(StatsList stats, Currency price)
            : this(stats, price, ItemQuality.Common, 0)
        {
        }
        public Item()
            : this(new StatsList(), new Currency(), ItemQuality.Common, 0) 
        {
        }

        public StatsList Stats
        {
            get { return this.stats; }
            private set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Stats can't be null.");
                }

                this.stats = value;
            }
        }

        public Currency SellPrice
        {
            get { return this.sellPrice; }
            private set
            {
                this.sellPrice = value;
            }
        }

        public ItemQuality ItemQuality
        {
            get { return this.itemQuality; }
            private set
            {
                this.itemQuality = value;
            }
        }
    }
}
