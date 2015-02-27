using RPG.Graphics.Map;

namespace RPG.GameLogic.Core
{
    public class CollisionHandler
    {
        public static bool CheckForObjectCollision(int additionalRow, int additionalCol, int positionX, int positionY)
        {
            int tileY = TileMap.GetTileY(positionY, Camera.Location.Y);
            int tileX = TileMap.GetTileX(positionX, Camera.Location.X);
            if (tileY + additionalRow < 0)
            {
                tileY = 1;
            }

            if (tileY + additionalRow >= TileMap.Rows.Count)
            {
                additionalRow = -1;
            }

            if (tileX + additionalCol < 0)
            {
                tileX = 1;
            }

            if (tileX + additionalCol >= TileMap.Rows[additionalRow].Columns.Count)
            {
                additionalCol = -1;
            }

            int tilePosition = TileMap.Rows[tileY + additionalRow].Columns[tileX + additionalCol].BaseTiles[0];

            if (tilePosition == 0 || tilePosition == 3)
            {
                return false;
            }

            return true;
        }

        public static bool CheckForEnemySpawnCollision(int positionX, int positionY)
        {
            int tileY = TileMap.GetEnemyTileY(positionY);
            int tileX = TileMap.GetEnemyTileX(positionX);
            int tilePosition = TileMap.Rows[tileY].Columns[tileX].BaseTiles[0];
            if (tilePosition == 0 || tilePosition == 3)
            {
                return false;
            }

            return true;
        }
    }
}
