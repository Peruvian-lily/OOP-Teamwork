using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG.Graphics.Map
{
    class TileMap
    {
        private string filename = "Content\\map.txt";
        public List<MapRow> Rows = new List<MapRow>();
        public static int MapWidth;
        public static int MapHeight;

        public TileMap()
        {

            var reader = new StreamReader(TitleContainer.OpenStream(this.filename));
            List<string> linesFromFile = new List<string>();

            string line = reader.ReadLine();
            MapWidth = line.Length;

            while (line != null)
            {
                MapHeight++;
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
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

            // Create Sample Map Data
           /* Rows[0].Columns[3].TileID = 3;
            Rows[0].Columns[4].TileID = 3;
            Rows[0].Columns[5].TileID = 1;
            Rows[0].Columns[6].TileID = 1;
            Rows[0].Columns[7].TileID = 1;

            Rows[1].Columns[3].TileID = 3;
            Rows[1].Columns[4].TileID = 1;
            Rows[1].Columns[5].TileID = 1;
            Rows[1].Columns[6].TileID = 1;
            Rows[1].Columns[7].TileID = 1;

            Rows[2].Columns[2].TileID = 3;
            Rows[2].Columns[3].TileID = 1;
            Rows[2].Columns[4].TileID = 1;
            Rows[2].Columns[5].TileID = 1;
            Rows[2].Columns[6].TileID = 1;
            Rows[2].Columns[7].TileID = 1;

            Rows[3].Columns[2].TileID = 3;
            Rows[3].Columns[3].TileID = 1;
            Rows[3].Columns[4].TileID = 1;
            Rows[3].Columns[5].TileID = 2;
            Rows[3].Columns[6].TileID = 2;
            Rows[3].Columns[7].TileID = 2;

            Rows[4].Columns[2].TileID = 3;
            Rows[4].Columns[3].TileID = 1;
            Rows[4].Columns[4].TileID = 1;
            Rows[4].Columns[5].TileID = 2;
            Rows[4].Columns[6].TileID = 2;
            Rows[4].Columns[7].TileID = 2;

            Rows[5].Columns[2].TileID = 3;
            Rows[5].Columns[3].TileID = 1;
            Rows[5].Columns[4].TileID = 1;
            Rows[5].Columns[5].TileID = 2;
            Rows[5].Columns[6].TileID = 2;
            Rows[5].Columns[7].TileID = 2;

            Rows[3].Columns[5].AddBaseTile(30);
            Rows[4].Columns[5].AddBaseTile(27);
            Rows[5].Columns[5].AddBaseTile(28);

            Rows[3].Columns[6].AddBaseTile(25);
            Rows[5].Columns[6].AddBaseTile(24);

            Rows[3].Columns[7].AddBaseTile(31);
            Rows[4].Columns[7].AddBaseTile(26);
            Rows[5].Columns[7].AddBaseTile(29);

            Rows[4].Columns[6].AddBaseTile(104);*/

            // End Create Sample Map Data
        }
    }
}
