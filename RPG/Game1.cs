#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using RPG.GameLogic.Core.Items;
using RPG.GameLogic.Models.NPC;
using RPG.GameLogic.Models.NPC.Base;
using RPG.GameLogic.Core;
using RPG.GameLogic.Models;
using RPG.Helpers;
using RPG.Helpers.CustomShapes;
using RPG.GameLogic.Core.Enemies;
using RPG.Helpers.Screens;

#endregion

namespace RPG
{
    public class GameStateManagementGame : Game
    {
        private GraphicsDeviceManager graphics;
        private ScreenManager screenManager;

        static readonly string[] preloadAssets =
        {
        };

        private SpriteBatch spriteBatch;


        public static int ScreenWidth;
        public static int ScreenHeight;

        private Engine engine;
        private Player player;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;



        public GameStateManagementGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 853;
            graphics.PreferredBackBufferHeight = 480;

            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            screenManager.AddScreen(new MainMenuScreen(), null);
       


            engine = Engine.GetInstance;
            player = new Player("placeholder", "placeholder",100,100,100,5);
            player.PickUp(ItemFactory.GenerateItem());
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            textDrawer = new TextDrawer(defaultFont);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
        }
    }
}
