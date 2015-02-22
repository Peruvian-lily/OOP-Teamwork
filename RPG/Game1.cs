#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.GameLogic.Core.Items;
using RPG.GameLogic.Core;
using RPG.GameLogic.Core.Enemies;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;
using RPG.Graphics;
using RPG.Graphics.Map;

#endregion

namespace RPG
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MainMenu mainMenu;
        private BattleScreen battleScreen;
        private Engine engine;
        private Player player;
        private List<Character> worldObjects = new List<Character>();
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;
        private Rectangle screen;

        public static readonly Vector2 UP_VECTOR = new Vector2(0, -1);
        public static readonly Vector2 DOWN_VECTOR = new Vector2(0, 1);
        public static readonly Vector2 RIGHT_VECTOR = new Vector2(1, 0);
        public static readonly Vector2 LEFT_VECTOR = new Vector2(-1, 0);
        public new static ContentManager Content;
        public static Random Rnd = new Random();
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static GameState GameState;
        private const int ENEMY_COUNT = 5;

        Map map = new Map();
        TileMap tileMap = new TileMap();
        int squaresAcross = 18;
        int squaresDown = 11;

        public Game1()
            : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
            Content = base.Content;
        }

        protected override void Initialize()
        {
            this.screen = new Rectangle(50, 50, 800, 600);
            ScreenWidth = this.GraphicsDevice.Viewport.Width;
            ScreenHeight = this.GraphicsDevice.Viewport.Height;
            GameState = GameState.MainMenu;
            this.mainMenu = new MainMenu();
            this.battleScreen = new BattleScreen();
            this.IsMouseVisible = true;
            base.Content = Content;

            this.engine = Engine.GetInstance;
            this.player = new Player("QueBabche", 100, 100, 100, 5);
            player.Position = new Vector2((float) screen.Width/2, (float) ScreenHeight/2);
            this.player.PickUp(ItemFactory.GenerateItem(25, 50));

            for (int i = 0; i < ENEMY_COUNT; i++)
            {
                int minPower = Rnd.Next(75);
                int maxPower = Rnd.Next(minPower, 101);
                bool hasBonus = Rnd.Next(1, 101) < 5;  // Currently has 5% chance to spawn enemy with bonus stuff.
                var enemy = EnemyFactory.SpawnEnemy(minPower, maxPower, hasBonus);

                int positionX = Rnd.Next(0, ScreenWidth - enemy.Animation.frameWidth);
                int positionY = Rnd.Next(0, ScreenHeight - enemy.Animation.frameHeight);
                enemy.Position = new Vector2(positionX, positionY);
                //engine.EnemyCollisionCheck(enemy, worldObjects);
                worldObjects.Add(enemy);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.defaultFont = base.Content.Load<SpriteFont>("Fonts\\Arial");
            this.mainMenu.LoadContent(base.Content);
            this.battleScreen.LoadContent(base.Content);
            this.textDrawer = new TextDrawer(this.defaultFont);
            Tile.TileSetTexture = Content.Load<Texture2D>(@"Tiles\tileset");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            /*
                textDrawer = new TextDrawer(defaultFont);
            */
            switch (GameState)
            {
                case GameState.InGame:
                    Camera.Update(tileMap, squaresAcross, squaresDown);
                    this.player.Update(gameTime);
                    foreach (var entry in worldObjects)
                    {
                        entry.Update(gameTime);
                    }
                    engine.EnemyCollisionCheck(player, worldObjects);
                    break;
                case GameState.MainMenu:
                    this.mainMenu.Update();
                    break;
                case GameState.Battle:
                    this.battleScreen.Update();
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);

            textDrawer.DrawString(spriteBatch, this.player.Position.ToString(), new Vector2(), Color.Black);

            this.spriteBatch.Begin();
            map.Draw(spriteBatch, tileMap, squaresAcross, squaresDown);

            switch (GameState)
            {
                case GameState.MainMenu:
                    this.mainMenu.Draw(this.spriteBatch);
                    break;
                case GameState.InGame:
                    this.player.Draw(this.spriteBatch);
                    worldObjects.ForEach(enemy =>
                    {
                        enemy.Draw(this.spriteBatch);
                    });
                    break;
                case GameState.Battle:
                    this.battleScreen.Draw(this.spriteBatch);
                    break;
            }
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
