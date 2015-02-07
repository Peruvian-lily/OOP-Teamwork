using System;
using RPG_GameLogic.Interfaces;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Enums;

namespace RPG_GameLogic.Items
{
    public class Boots : WearableItem
    {
        public Boots(StatsList statsList, Currency price, ItemQuality itemQuality, int id)
            : base(statsList, price, itemQuality, id)
        {
        }
        public Boots(StatsList statsList, Currency price, ItemQuality itemQuality)
            : this(statsList, price, itemQuality, 0)
        {
        }
        public Boots(StatsList statsList, Currency price)
            : this(statsList, price, ItemQuality.Common, 0)
        {
        }
        public Boots()
            : this(new StatsList(), new Currency(), ItemQuality.Common, 0)
        {
        }
    }
}
