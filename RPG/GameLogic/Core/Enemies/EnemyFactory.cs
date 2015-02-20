using RPG.GameLogic.Models.Characters;

namespace RPG.GameLogic.Core.Enemies
{
    using System;
    using System.Collections.Generic;
    using Models.Stats.Base;
    using Models.Stats;

    public static class EnemyFactory
    {
        private static string id = "1111";
        private static Random rnd = new Random();

        public static Enemy GenerateEnemy(int minValue, int maxValue)
        {
            string enemyName = GenerateEnemyName();
            int power = rnd.Next(minValue, maxValue - minValue);
            List<Stat> stats = GenerateStats(power);
            int health = rnd.Next(minValue, maxValue - minValue);
            int attack = rnd.Next(minValue, maxValue - minValue);
            int defense = rnd.Next(minValue, maxValue - minValue);

            Enemy enemy = new Enemy(id, enemyName, health, attack, defense, stats);
            
            return enemy;
        }

        private static List<Stat> GenerateStats(int power)
        {
            List<Stat> allStats = new List<Stat>
            {
                new Attack(rnd.Next(power) + 1),
                new FireAttack(rnd.Next(power) + 1),
                new Health(rnd.Next(power) + 1),
                new Defense(rnd.Next(power) + 1),
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
