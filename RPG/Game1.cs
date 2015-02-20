#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using RPG.GameLogic.Core.Items;
using RPG.GameLogic.Models.NPC;
using RPG.GameLogic.Core;
using RPG.GameLogic.Models;
using RPG.Helpers;
using RPG.Helpers.CustomShapes;
using RPG.GameLogic.Core.Enemies;
using RPG.GameLogic.Models.Stats.Base;

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
        private Enemy enemy;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;
        private Rectangle screen;

        public static readonly Vector2 UP_VECTOR = new Vector2(0, -1);
        public static readonly Vector2 DOWN_VECTOR = new Vector2(0, 1);
        public static readonly Vector2 RIGHT_VECTOR = new Vector2(1, 0);
        public static readonly Vector2 LEFT_VECTOR = new Vector2(-1, 0);
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static GameState GameState;
        public new static ContentManager Content;

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
            this.player = new Player("placeholder", "placeholder", 100, 100, 100, 5);
            this.enemy = new Enemy("placeholder", "placeholder", 100, 100, 100, new List<Stat>() { });
            enemy.Position = new Vector2(200, 200);

            this.player.PickUp(ItemFactory.GenerateItem());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.defaultFont = base.Content.Load<SpriteFont>("Fonts\\Arial");
            this.mainMenu.LoadContent(base.Content);
            this.battleScreen.LoadContent(base.Content);


            // This is the place to initialize all variables depending on external resources.
            this.textDrawer = new TextDrawer(this.defaultFont);
        }

        protected override void UnloadContent()
        {
        }

        private void CheckCollisons(Player player, Enemy enemy)
        {
            Vector2 topLeftPlayer = new Vector2(player.Position.X, player.Position.Y);
            Vector2 topRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y);
            Vector2 bottomLeftPlayer = new Vector2(player.Position.X, player.Position.Y + player.Height);
            Vector2 bottomRightPlayer = new Vector2(player.Position.X + player.Width, player.Position.Y + player.Height);

            int enemyWidth = enemy.Animation.frameWidth;
            int enemyHeight = enemy.Animation.frameHeight;
            int enemyX = (int)enemy.Position.X;
            int enemyY = (int)enemy.Position.Y;
            Rectangle enemyCollisonRect = new Rectangle(enemyX, enemyY + enemyHeight, enemyWidth, enemyHeight);

            if (IsPointInRect(topLeftPlayer, enemyCollisonRect))
            {
               GameState = GameState.Battle;
            }
            else if (IsPointInRect(topRightPlayer, enemyCollisonRect))
            {
                GameState = GameState.Battle;
            }
            else if (IsPointInRect(bottomLeftPlayer, enemyCollisonRect))
            {
                GameState = GameState.Battle;
            }
            else if (IsPointInRect(bottomRightPlayer, enemyCollisonRect))
            {
               GameState = GameState.Battle;
            }
        }

        public static bool IsPointInRect(Vector2 point, Rectangle rectangle)
        {
            bool isPointXIn = point.X >= rectangle.X && point.X <= rectangle.X + rectangle.Width;
            bool isPointYIn = point.Y <= rectangle.Y && point.Y >= rectangle.Y - rectangle.Height;
            return isPointXIn && isPointYIn;
        }


        protected override void Update(GameTime gameTime)
        {
            /*
                textDrawer = new TextDrawer(defaultFont);
            */


            CheckCollisons(player, enemy);
            this.mainMenu.Update();
            this.battleScreen.Update();
            this.player.Update(gameTime);
            this.enemy.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);
            this.spriteBatch.Begin();
            switch (GameState)
            {
                case GameState.MainMenu:
                    this.mainMenu.Draw(this.spriteBatch);
                    break;
                case GameState.InGame:
                    this.player.Draw(this.spriteBatch);
                    this.enemy.Draw(this.spriteBatch);
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
