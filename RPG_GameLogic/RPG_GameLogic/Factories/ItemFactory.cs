using System;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Items;
using RPG_GameLogic.Enums;

namespace RPG_GameLogic.Factories
{
    public static class ItemFactory
    {
        private static int idCounter = 0;

        public static StatsList CreateStatsListWithDefaultValues()
        {
            AttackStats attackStats = new AttackStats(0, 0, 0, 0, 0);
            DefenceStats defenceStats = new DefenceStats(0, 0, 0, 0, 0);
            BodyStats body = new BodyStats(0, 0);
            CritStats crit = new CritStats(0, 0);
            StatsList stats = new StatsList(attackStats, defenceStats, body, crit);
            return stats;
        }

        public static T CreateWearableItemWithDefaultValues<T>() where T : Item
        {
            idCounter++;
            StatsList stats = CreateStatsListWithDefaultValues();
            T item = (T)Activator.CreateInstance(typeof(T), new object[] {});
            return item;
        }
    }
}
