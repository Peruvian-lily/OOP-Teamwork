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

        public void EnemyCollisionCheck(Player player, List<Enemy> collidableObjects)
        {
            foreach (var obj in collidableObjects)
            {
                Rectangle collisionRect = obj.CollisionRect;

                if (obj is Enemy)
                {
                    EnemyCollisions(player, collisionRect);
                }
            }
        }

        private void EnemyCollisions(Player player, Rectangle collisionRect)
        {
            Vector2 topLeftPlayer = new Vector2(player.Position.X, player.Position.Y);
            Vector2 topRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y);
            Vector2 bottomLeftPlayer = new Vector2(player.Position.X, player.Position.Y + player.Height);
            Vector2 bottomRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y + player.Height);

            if (IsPointInRect(topLeftPlayer, collisionRect))
            {
                Game1.GameState = GameState.Battle;
            }
            else if (IsPointInRect(topRightPlayer, collisionRect))
            {
                Game1.GameState = GameState.Battle;
            }
            else if (IsPointInRect(bottomLeftPlayer, collisionRect))
            {
                Game1.GameState = GameState.Battle;
            }
            else if (IsPointInRect(bottomRightPlayer, collisionRect))
            {
                Game1.GameState = GameState.Battle;
            }
        }

        private bool IsPointInRect(Vector2 point, Rectangle rectangle)
        {
            bool isPointXIn = point.X >= rectangle.X && point.X <= rectangle.X + rectangle.Width;
            bool isPointYIn = point.Y <= rectangle.Y && point.Y >= rectangle.Y - rectangle.Height;
            return isPointXIn && isPointYIn;
        }
    }
}
