using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RPG.GameLogic.Models.Characters;

namespace RPG.Graphics.Map
{
    static class TileMap
    {
        public const int CameraOffSetY = 576;
        public const int CameraInitialY = 5;
        public const int CameraOffSetX = 864;
        public const int CameraInitialX = 9;
        private static string filename = "Content\\Levels\\level2.txt";
        public static List<MapRow> Rows = new List<MapRow>();
        public static int mapWidth;
        public static int mapHeight;

        public static int GetTileY(Player player, double cameraPositionY)
        {
            int tileY = 0;
            int cameraY = (int)Math.Round(cameraPositionY);
            if (player.Position.Y <= Game1.ScreenHeight / 2f && cameraY == 0)
            {
                tileY = (int)player.Position.Y / Tile.TileHeight;
            }
            else if (player.Position.Y == Game1.ScreenHeight / 2f && cameraY > 0)
            {
                tileY = (cameraY / Tile.TileHeight) + CameraInitialY;
            }
            else if (player.Position.Y > Game1.ScreenHeight / 2f && cameraY > 0)
            {
                tileY = TileMap.mapHeight - ((CameraOffSetY - (int)player.Position.Y) / Tile.TileHeight);
            }

            return tileY;
        }

        public static int GetTileX(Player player, double cameraPositionX)
        {
            int tileX = 0;
            int cameraX = (int)Math.Round(cameraPositionX);
            if (player.Position.X <= Game1.ScreenWidth / 2f && cameraX == 0)
            {
                tileX = (int)player.Position.X / Tile.TileWidth;
            }
            else if (player.Position.X == Game1.ScreenWidth / 2f && cameraX > 0)
            {
                tileX = (cameraX / Tile.TileWidth) + CameraInitialX;
            }
            else if (player.Position.X > Game1.ScreenWidth / 2f && cameraX > 0)
            {
                tileX = TileMap.mapWidth - ((CameraOffSetX - (int)player.Position.X) / Tile.TileWidth);
            }

            return tileX;
        }

        static TileMap()
        {
            var reader = new StreamReader(TitleContainer.OpenStream(filename));
            string line = reader.ReadLine();
            mapWidth = line.Length;

            while (line != null)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < mapWidth; x++)
                {
                    char currentSymbol = line[x];
                    switch (currentSymbol)
                    {
                        case 'A':
                            thisRow.Columns.Add(new MapCell(0));
                            break;
                        case 'B':
                            thisRow.Columns.Add(new MapCell(1));
                            break;
                        case 'C':
                            thisRow.Columns.Add(new MapCell(2));
                            break;
                        case 'D':
                            thisRow.Columns.Add(new MapCell(3));
                            break;
                        case 'E':
                            thisRow.Columns.Add(new MapCell(4));
                            break;
                        case 'F':
                            thisRow.Columns.Add(new MapCell(5));
                            break;
                        case 'G':
                            thisRow.Columns.Add(new MapCell(6));
                            break;
                        case 'H':
                            thisRow.Columns.Add(new MapCell(7));
                            break;
                        default:
                            throw new ArgumentException("Invalid symbol!");
                    }
                }
                Rows.Add(thisRow);
                line = reader.ReadLine();
            }

            mapHeight = Rows.Count;
            reader.Close();
        }
    }
}
