using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;

namespace RPG.Graphics.Map
{
    static class Camera
    {
        private const int ScreenSizeRemainder = 1;
        public static readonly int EnemyMaxWidth = TileMap.mapWidth * Tile.TileWidth;
        public static readonly int EnemyMaxHeight = TileMap.mapHeight * Tile.TileHeight;
        public static readonly int CameraMaxWidth = (TileMap.mapWidth - ScreenSizeRemainder) * Tile.TileWidth - Game1.bufferWidth;
        public static readonly int CameraMaxHeight = (TileMap.mapHeight - ScreenSizeRemainder) * Tile.TileHeight -
                                              Game1.bufferHeight;

        public static Vector2 Location = Vector2.Zero;

        public static void Update(int squaresAcross, int squaresDown, Player player, List<Character> enemies)
        {
            KeyboardState ks = Keyboard.GetState();

            if (player.Position.X == Game1.ScreenWidth / 2f)
            {
                if (ks.IsKeyDown(Keys.Left) && !player.CheckForObjectCollision(0, -1))
                {
                    Camera.Location.X = MathHelper.Clamp(Camera.Location.X - Player.Speed, 0, (TileMap.mapWidth - squaresAcross) * Tile.TileWidth);
                    foreach (var enemy in enemies)
                    {
                        enemy.Position.X = MathHelper.Clamp(enemy.Position.X + Player.Speed, enemy.Position.X, EnemyMaxWidth);
                    }
                }

                if (ks.IsKeyDown(Keys.Right) && !player.CheckForObjectCollision(0, 0))
                {
                    Camera.Location.X = MathHelper.Clamp(Camera.Location.X + Player.Speed, 0, TileMap.mapWidth * Tile.TileWidth);
                    foreach (var enemy in enemies)
                    {
                        enemy.Position.X = MathHelper.Clamp(enemy.Position.X - Player.Speed, CameraMaxWidth * -1, TileMap.mapWidth * Tile.TileWidth);
                    }
                }
            }

            if (player.Position.Y == Game1.ScreenHeight / 2f)
            {
                if (ks.IsKeyDown(Keys.Up) && !player.CheckForObjectCollision(0, 0))
                {
                    Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - Player.Speed, 0, (TileMap.mapHeight - squaresDown) * Tile.TileHeight);
                    foreach (var enemy in enemies)
                    {
                        enemy.Position.Y = MathHelper.Clamp(enemy.Position.Y + Player.Speed, enemy.Position.Y, EnemyMaxHeight);
                    }
                }

                if (ks.IsKeyDown(Keys.Down) && !player.CheckForObjectCollision(1, 0))
                {
                    Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + Player.Speed, 0, (TileMap.mapHeight - squaresDown) * Tile.TileHeight);
                    foreach (var enemy in enemies)
                    {
                        enemy.Position.Y = MathHelper.Clamp(enemy.Position.Y - Player.Speed, CameraMaxHeight * -1, TileMap.mapWidth * Tile.TileWidth);
                    }
                }
            }
        }
    }
}
