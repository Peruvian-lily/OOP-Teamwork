using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Graphics.Map
{
    class Map
    {
/*        public LinkedList<Tile> tiles;
        public LinkedList<Enemy> enemies;

        public */

        public void Draw(SpriteBatch spriteBatch, int squaresAcross, int squaresDown)
        {
            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileWidth, Camera.Location.Y / Tile.TileHeight);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileWidth, Camera.Location.Y % Tile.TileHeight);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            for (int y = 0; y < squaresDown; y++)
            {
                for (int x = 0; x < squaresAcross; x++)
                {
                    foreach (int tileID in TileMap.Rows[y + firstY].Columns[x + firstX].BaseTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            new Rectangle(
                                (x * Tile.TileWidth) - offsetX, (y * Tile.TileHeight) - offsetY,
                                Tile.TileWidth, Tile.TileHeight),
                            Tile.GetSourceRectangle(tileID),
                            Color.White);
                    }
                }
            }
        }

        public int squaresDown { get; set; }

        public int squaresAcross { get; set; }

    }
}


 //                       spriteBatch.Draw(
 //                       Tile.TileSetTexture,
 //                       new Rectangle((x * 32) - offsetX, (y * 32) - offsetY, 32, 32),
 //                       Tile.GetSourceRectangle(tileMap.Rows[y + firstY].Columns[x + firstX].TileID),
 //                       Color.White);