namespace RPG.GameLogic.Core.Enemies
{
    using RPG.GameLogic.Models.NPC;
    using System;
    using System.Collections.Generic;

    public static class EnemyFactory
    {
        private static string id = "1111";
        private static Random rnd = new Random();

        public static Enemy GenerateEnemy()
        {
            EnemyType enemyType = (EnemyType)rnd.Next(1);

            return null;
        }
    }
}
