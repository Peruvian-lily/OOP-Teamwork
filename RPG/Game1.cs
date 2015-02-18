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
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;
        public static GameState GameState;
        private Rectangle screen;

        public static int ScreenWidth;
        public static int ScreenHeight;
        public new static ContentManager Content;

        public Game1(): base()
        {
            graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
            Content = base.Content;
        }

        protected override void Initialize()
        {
            screen = new Rectangle(50, 50, 800, 600);
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            GameState = GameState.MainMenu;
            mainMenu = new MainMenu();
            this.IsMouseVisible = true;
            base.Content = Content;

            engine = Engine.GetInstance;
            player = new Player("placeholder", "placeholder", 100, 100, 100, 5);
            player.PickUp(ItemFactory.GenerateItem());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultFont = base.Content.Load<SpriteFont>("Fonts\\Arial");
            mainMenu.LoadContent(base.Content);
            



            // This is the place to initialize all variables depending on external resources.
            textDrawer = new TextDrawer(defaultFont);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
/*
            textDrawer = new TextDrawer(defaultFont);
*/
            mainMenu.Update();
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (GameState)
            {
                case GameState.MainMenu: mainMenu.Draw(spriteBatch);
                    break;
                case GameState.InGame: player.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
