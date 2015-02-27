namespace RPG.Graphics.Map
{
    using System.Collections.Generic;

    class MapCell
    {
        public List<int> BaseTiles = new List<int>();

        public MapCell(int tileID)
        {
            this.TileID = tileID;
        }

        public int TileID
        {
            get
            {
                return this.BaseTiles.Count > 0 ? this.BaseTiles[0] : 0;
            }

            set
            {
                if (this.BaseTiles.Count > 0)
                {
                    this.BaseTiles[0] = value;
                }
                else
                {
                    this.AddBaseTile(value);
                }
            }
        }

        public void AddBaseTile(int tileID)
        {
            this.BaseTiles.Add(tileID);
        }
    }
}
