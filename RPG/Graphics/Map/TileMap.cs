using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG.Graphics.Map
{
    static class TileMap
    {
        private static string filename = "Content\\Levels\\map.txt";
        public static List<MapRow> Rows = new List<MapRow>();
        public static int mapWidth;
        public static int mapHeight;

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
                    if (line[x] == 'A')
                    {
                        thisRow.Columns.Add(new MapCell(0));
                    }
                    if (line[x] == 'B')
                    {
                        thisRow.Columns.Add(new MapCell(1));
                    }
                    if (line[x] == 'C')
                    {
                        thisRow.Columns.Add(new MapCell(2));
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
