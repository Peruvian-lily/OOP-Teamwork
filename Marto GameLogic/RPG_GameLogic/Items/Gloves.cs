using System;
using RPG_GameLogic.Interfaces;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Enums;

namespace RPG_GameLogic.Items
{
    public class Gloves : WearableItem
    {
        public Gloves(StatsList statsList, Currency price, ItemQuality itemQuality, int id)
            : base(statsList, price, itemQuality, id)
        {
        }
        public Gloves(StatsList statsList, Currency price, ItemQuality itemQuality)
            : this(statsList, price, itemQuality, 0)
        {
        }
        public Gloves(StatsList statsList, Currency price)
            : this(statsList, price, ItemQuality.Common, 0)
        {
        }
        public Gloves()
            : this(new StatsList(), new Currency(), ItemQuality.Common, 0)
        {
        }
    }
}
