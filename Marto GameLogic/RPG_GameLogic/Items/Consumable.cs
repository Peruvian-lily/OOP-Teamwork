using System;
using RPG_GameLogic.Interfaces;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Enums;

namespace RPG_GameLogic.Items
{
    public abstract class Consumable : Item
    {
        public Consumable(StatsList statsList, Currency price, int id)
            : base(statsList, price, ItemQuality.Common, id)
        {
        }
        public Consumable(StatsList statsList, Currency price)
            : this(statsList, price, 0)
        {   
        }
        public Consumable()
            : this(new StatsList(), new Currency(), 0)
        {
        }
    }
}
