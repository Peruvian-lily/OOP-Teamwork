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
        private static string filename = "Content\\Levels\\level2.txt";
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
