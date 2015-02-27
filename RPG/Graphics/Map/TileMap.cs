namespace RPG.Graphics.Map
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Xna.Framework;

    static class TileMap
    {
        public const int CameraOffSetY = 576;
        public const int CameraInitialY = 5;
        public const int CameraOffSetX = 864;
        public const int CameraInitialX = 9;
        public static List<MapRow> Rows = new List<MapRow>();
        public static int mapWidth;
        public static int mapHeight;
        private static string filename = "Content\\Levels\\level1.txt";

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

        public static int GetPlayerTileY(int position, double cameraPositionY)
        {
            int tileY = 0;
            int cameraY = (int)Math.Round(cameraPositionY);
            if (position <= Game1.ScreenHeight / 2f && cameraY == 0)
            {
                tileY = (int)position / Tile.TileHeight;
            }
            else if (position == Game1.ScreenHeight / 2f && cameraY > 0)
            {
                tileY = (cameraY / Tile.TileHeight) + CameraInitialY;
            }
            else if (position > Game1.ScreenHeight / 2f && cameraY > 0)
            {
                tileY = TileMap.mapHeight - ((CameraOffSetY - (int)position) / Tile.TileHeight);
            }

            return tileY;
        }

        public static int GetPlayerTileX(int position, double cameraPositionX)
        {
            int tileX = 0;
            int cameraX = (int)Math.Round(cameraPositionX);
            if (position <= Game1.ScreenWidth / 2f && cameraX == 0)
            {
                tileX = (int)position / Tile.TileWidth;
            }
            else if (position == Game1.ScreenWidth / 2f && cameraX > 0)
            {
                tileX = (cameraX / Tile.TileWidth) + CameraInitialX;
            }
            else if (position > Game1.ScreenWidth / 2f && cameraX > 0)
            {
                tileX = TileMap.mapWidth - ((CameraOffSetX - (int)position) / Tile.TileWidth);
            }

            return tileX;
        }

        public static int GetEnemyTileY(int position)
        {
            return position / Tile.TileWidth;
        }

        public static int GetEnemyTileX(int position)
        {
            return position / Tile.TileHeight;
        }
    }
}
