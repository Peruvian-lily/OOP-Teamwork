using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RPG.GameLogic.Models.Characters;

namespace RPG.Graphics.Map
{
    static class Camera
    {
        public static Vector2 Location = Vector2.Zero;

        public static void Update(int squaresAcross, int squaresDown, Player player)
        {
            KeyboardState ks = Keyboard.GetState();

            if (player.Position.X == Game1.ScreenWidth / 2f)
            {
                if (ks.IsKeyDown(Keys.Left))
                {
                    Camera.Location.X = MathHelper.Clamp(Camera.Location.X - Player.speed, 0, (TileMap.mapWidth - squaresAcross) * Tile.TileWidth);
                }

                if (ks.IsKeyDown(Keys.Right))
                {
                    Camera.Location.X = MathHelper.Clamp(Camera.Location.X + Player.speed, 0, TileMap.mapWidth * Tile.TileWidth);

                }
            }

            if (player.Position.Y == Game1.ScreenHeight / 2f)
            {
                if (ks.IsKeyDown(Keys.Up))
                {
                    Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - Player.speed, 0, (TileMap.mapHeight - squaresDown) * Tile.TileHeight);
                }

                if (ks.IsKeyDown(Keys.Down))
                {
                    Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + Player.speed, 0, (TileMap.mapHeight - squaresDown) * Tile.TileHeight);
                }
            }
            
        }
    }
}
