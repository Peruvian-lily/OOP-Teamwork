using System;
using System.Collections.Generic;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Core.Factory
{
    public static class EnemyFactory
    {
        private static Random rnd = new Random();

        public static Enemy SpawnEnemy(int minValue, int maxValue, bool hasStats)
        {
            string enemyName = GenerateEnemyName();
            //int power = rnd.Next(minValue, maxValue + 1);
            int health = rnd.Next(minValue, maxValue + 1);
            int attack = rnd.Next(minValue, maxValue + 1);
            int defense = rnd.Next(minValue, maxValue + 1)/2;
            var stats = new List<Stat>();
            if (hasStats)
            {
                stats = GenerateStats(minValue, maxValue);
            }
            Enemy enemy = new Enemy(enemyName, health, attack, defense, stats, EnemyType.Freak);
            return enemy;
        }

        private static List<Stat> GenerateStats(int minValue, int maxValue)
        {
            List<Stat> allStats = new List<Stat>
            {
                new Elemental(rnd.Next(minValue, maxValue + 1), StatType.Defensive),
                new Elemental(rnd.Next(minValue, maxValue + 1) + 1, StatType.Offensive),
            };

            List<Stat> returnStats = new List<Stat>();

            int randomStatIndex1 = rnd.Next(allStats.Count);
            int randomStatIndex2 = rnd.Next(allStats.Count);

            while (randomStatIndex2 == randomStatIndex1)
            {
                randomStatIndex2 = rnd.Next(allStats.Count);
            }

            returnStats.Add(allStats[randomStatIndex1]);
            returnStats.Add(allStats[randomStatIndex2]);

            return returnStats;
        }

        private static string GenerateEnemyName()
        {
            List<string> prefix = new List<string>
            {
                "Ugly",
                "Stupid",
                "Angry",
                "Deadly",
                "Weakly",
                "Greedy"
            };

            List<string> suffix = new List<string>
            {
                "Cucumber",
                "Tomato",
                "Apple",
                "Lettuce",
                "Pepper",
                "Carrot"
            };

            List<string> name = new List<string>
            {
                "Klug",
                "Dumb",
                "Flubgok",
                "Bridiot",
                "Mumgunk",
                "Airbone"
            };

            int prefixIndex = rnd.Next(prefix.Count);
            int suffixIndex = rnd.Next(suffix.Count);
            int nameIndex = rnd.Next(name.Count);
            string fullName = string.Format("{0} {1} {2}", prefix[prefixIndex], name[nameIndex], suffix[suffixIndex]);
            return fullName;
        }
    }
}
