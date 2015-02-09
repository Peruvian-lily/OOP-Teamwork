namespace RPG.GameLogic.Core.Enemies
{
    using System;
    using System.Collections.Generic;
    using RPG.GameLogic.Models.NPC;
    using RPG.GameLogic.Models.Stats.Base;

    public static class EnemyFactory
    {
        private static string id = "1111";
        private static Random rnd = new Random();

        public static Enemy GenerateEnemy(int minValue, int maxValue)
        {
            string enemyName = GenerateEnemyName();
            List<Stat> stats = GenerateStats();
            int health = rnd.Next(minValue, maxValue - minValue);
            int attack = rnd.Next(minValue, maxValue - minValue);
            int defense = rnd.Next(minValue, maxValue - minValue);

            Enemy enemy = new Enemy(id, enemyName, health, attack, defense, stats);
            
            return enemy;
        }

        private static List<Stat> GenerateStats()
        {
            return null;
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
