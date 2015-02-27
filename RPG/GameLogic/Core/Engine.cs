using RPG.GameLogic.Enums;

namespace RPG.GameLogic.Core
{
    using System.Collections.Generic;
    using RPG.GameLogic.Models.Characters.Base;
    using Microsoft.Xna.Framework;
    using RPG.GameLogic.Models.Characters;
    using RPG.Graphics;

    public class Engine
    {
        private static Engine instance;

        private Engine()
        {
        }

        public static Engine GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }

                return instance;
            }
        }

        public void EnemyCollisionCheck(Player player, List<Character> collidableObjects)
        {
            collidableObjects.ForEach(obj =>
            {
                if (obj is Enemy)
                {
                    Rectangle collisionRect = ((Enemy)obj).CollisionRect;
                    this.EnemyCollisions(player, collisionRect, (Enemy)obj, collidableObjects);
                }
            });
        }

        private void EnemyCollisions(Player player, Rectangle collisionRect, Enemy enemy, List<Character> worldObjects)
        {
            Vector2 topLeftPlayer = new Vector2(player.Position.X, player.Position.Y);
            Vector2 topRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y);
            Vector2 bottomLeftPlayer = new Vector2(player.Position.X, player.Position.Y + player.Height);
            Vector2 bottomRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y + player.Height);

            if (collisionRect.IsPointInRect(topLeftPlayer) ||
                collisionRect.IsPointInRect(topRightPlayer) ||
                collisionRect.IsPointInRect(bottomLeftPlayer) ||
                collisionRect.IsPointInRect(bottomRightPlayer))
            {
                Game1.CurrentState = GameState.BattleScreenState;
                CollisionResult.enemy = enemy;
                CollisionResult.player = player;
                enemy.GetAllies(worldObjects);
            }
        }
    }
}
