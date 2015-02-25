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
                    EnemyCollisions(player.CollisionRect, collisionRect);
                }
            });
        }

        private void EnemyCollisions(Rectangle playerCollisionRect, Rectangle collisionRect)
        {
            if (playerCollisionRect.Intersects(collisionRect))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
            else if (playerCollisionRect.Intersects(collisionRect))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
            else if (playerCollisionRect.Intersects(collisionRect))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
            else if (playerCollisionRect.Intersects(collisionRect))
            {
                Game1.CurrentState = GameState.BattleScreenState;
            }
        }
    }
}
