using RPG.GameLogic.Models.Characters.Base;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RPG.GameLogic.Models.Characters;
using RPG;
using RPG.GameLogic.Models;
using RPG.Graphics;

namespace RPG.GameLogic.Core
{
    public class Engine
    {
        private static Engine _instance;

        public static Engine GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Engine();
                }

                return _instance;
            }
        }

        private Engine()
        {
        }

        public void EnemyCollisionCheck(Player player, List<Character> collidableObjects)
        {
            collidableObjects.ForEach(obj =>
            {
                if (obj is Enemy)
                {
                    Rectangle collisionRect = ((Enemy) obj).CollisionRect;
                    EnemyCollisions(player, collisionRect);
                }
            });
        }

        private void EnemyCollisions(Player player, Rectangle collisionRect)
        {
            Vector2 topLeftPlayer = new Vector2(player.Position.X, player.Position.Y);
            Vector2 topRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y);
            Vector2 bottomLeftPlayer = new Vector2(player.Position.X, player.Position.Y + player.Height);
            Vector2 bottomRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y + player.Height);

            if (collisionRect.IsPointInRect(topLeftPlayer))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
            else if (collisionRect.IsPointInRect(topRightPlayer))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
            else if (collisionRect.IsPointInRect(bottomLeftPlayer))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
            else if (collisionRect.IsPointInRect(bottomRightPlayer))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
        }
    }
}
