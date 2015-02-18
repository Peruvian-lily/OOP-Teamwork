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

        private Engine engine;
        private Player player;
        private Enemy enemy;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;
        public static GameState GameState;
        private Rectangle screen;

        public static int ScreenWidth;
        public static int ScreenHeight;
        public new static ContentManager Content;

        public Game1(): base()
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
            this.IsMouseVisible = true;
            base.Content = Content;

            this.engine = Engine.GetInstance;
            this.player = new Player("placeholder", "placeholder", 100, 100, 100, 5);
            this.enemy = new Enemy("placeholder", "placeholder", 100, 100, 100, new List<Stat>(){});

            this.player.PickUp(ItemFactory.GenerateItem());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.defaultFont = base.Content.Load<SpriteFont>("Fonts\\Arial");
            this.mainMenu.LoadContent(base.Content);


            // This is the place to initialize all variables depending on external resources.
            this.textDrawer = new TextDrawer(this.defaultFont);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
/*
            textDrawer = new TextDrawer(defaultFont);
*/
            this.mainMenu.Update();
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
            }
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
