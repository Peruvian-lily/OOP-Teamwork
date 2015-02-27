namespace RPG.Graphics.GameStates
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using RPG.GameLogic.Core;
    using RPG.GameLogic.Core.Factory;
    using RPG.GameLogic.Models.Characters;
    using RPG.GameLogic.Models.Characters.Base;
    using RPG.Graphics.Map;

    class GamePlayState : AbstractGameState
    {
        private const int EnemyCount = 10;
        private const int MapSquaresAcross = 18;
        private const int MapSquaresDown = 11;

        private static Random Rnd;
        private Player player;
        private List<Character> worldObjects;
        private Map map;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;
        private Engine engine;

        public GamePlayState(Game game, Player player, List<Character> worldObjects)
            : base(game)
        {
            this.map = new Map();
            Tile.TileSetTexture = Game1.Content.Load<Texture2D>(@"Tiles\tileset");
            this.worldObjects = worldObjects;
            Rnd = new Random();
            this.EnemySpawn(EnemyCount);
            this.player = player;
            this.defaultFont = Game1.Content.Load<SpriteFont>("Fonts\\Arial");
            this.textDrawer = new TextDrawer(this.defaultFont);
        }

        public void EnemySpawn(int enemyCount)
        {
            while (this.worldObjects.Count < enemyCount)
            {
                int minPower = Rnd.Next(75);
                int maxPower = Rnd.Next(minPower, 101);

                // Currently has 5% chance to spawn enemy with bonus stuff.
                bool hasBonus = Rnd.Next(1, 101) < 5;
                var enemy = EnemyFactory.SpawnEnemy(minPower, maxPower, hasBonus);
                int positionX = Rnd.Next(0, (Camera.CameraMaxWidth + Game1.BufferWidth) - enemy.Animation.FrameWidth);
                int positionY = Rnd.Next(0, (Camera.CameraMaxHeight + Game1.BufferHeight) - enemy.Animation.FrameHeight);
                if (!CollisionHandler.CheckForEnemySpawnCollision(positionX, positionY))
                {
                    enemy.Position = new Vector2(positionX, positionY);
                    this.worldObjects.Add(enemy);
                    this.engine = Engine.GetInstance;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Update(MapSquaresAcross, MapSquaresDown, this.player, this.worldObjects);
            this.player.Update(gameTime);
            foreach (var entry in this.worldObjects)
            {
                entry.Update(gameTime);
            }

            this.engine.EnemyCollisionCheck(this.player, this.worldObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int tileY = TileMap.GetPlayerTileY((int)this.player.Position.Y, (int)Camera.Location.Y);
            int tileX = TileMap.GetPlayerTileX((int)this.player.Position.X, (int)Camera.Location.X);
            spriteBatch.Begin();
            this.map.Draw(spriteBatch, MapSquaresAcross, MapSquaresDown);
            this.player.Draw(spriteBatch);
            this.worldObjects.ForEach(enemy =>
            {
                enemy.Draw(spriteBatch);
            });
            string health = string.Format("QueBapche Health: {0}", player.Health.Value);
            this.textDrawer.DrawString(spriteBatch, health, new Vector2(0, 0), Color.BlanchedAlmond);
            spriteBatch.End();
        }
    }
}
