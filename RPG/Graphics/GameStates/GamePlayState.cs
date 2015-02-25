using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Core;
using RPG.GameLogic.Core.Factory;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;
using RPG.Graphics.Map;

namespace RPG.Graphics.GameStates
{
    class GamePlayState : AbstractGameState
    {
        private const int EnemyCount = 100;
        private const int MapSquaresAcross = 18;
        private const int MapSquaresDown = 11;

        private static Random Rnd;
        private Player player;
        private List<Character> worldObjects;
        private Map.Map map;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;
        private Engine engine;

        public GamePlayState(Game game, Player player, List<Character> worldObjects )
            : base(game)
        {
            map = new Map.Map();
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
            for (int i = 0; i < enemyCount; i++)
            {
                int minPower = Rnd.Next(75);
                int maxPower = Rnd.Next(minPower, 101);

                // Currently has 5% chance to spawn enemy with bonus stuff.
                bool hasBonus = Rnd.Next(1, 101) < 5;  
                var enemy = EnemyFactory.SpawnEnemy(minPower, maxPower, hasBonus);
                int positionX = Rnd.Next(0, Camera.CameraMaxWidth - enemy.Animation.FrameWidth);
                int positionY = Rnd.Next(0, Camera.CameraMaxHeight - enemy.Animation.FrameHeight);
                enemy.Position = new Vector2(positionX, positionY);
                this.worldObjects.Add(enemy);
                engine = Engine.GetInstance;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Update(MapSquaresAcross, MapSquaresDown, player);
            this.player.Update(gameTime);
            foreach (var entry in this.worldObjects)
            {
                entry.Update(gameTime);
            }
            engine.EnemyCollisionCheck(player, worldObjects);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            map.Draw(spriteBatch, MapSquaresAcross, MapSquaresDown);
            this.player.Draw(spriteBatch);
            this.worldObjects.ForEach(enemy =>
            {
                enemy.Draw(spriteBatch);
            });
            this.textDrawer.DrawString(spriteBatch, player.Position.X + "/" + this.player.Position.Y);
            string cameraLocation = string.Format("         Camera x: {0}. Camera y: {1}.", Camera.Location.X, Camera.Location.Y);
            this.textDrawer.DrawString(spriteBatch, cameraLocation);
            spriteBatch.End();
        }
    }
}
