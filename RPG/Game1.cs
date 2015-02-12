﻿#region Using Statements

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
using RPG.GameLogic.Models.NPC.Base;
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
        private MainMenu mainMenu = new MainMenu();
        private KeyboardState currentKeyboardState;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private Engine engine;
        private Player player;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;
        public static GameState GameState;


        public Game1()
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

            engine = Engine.GetInstance;
            player = new Player("placeholder", "placeholder",100,100,100,5);
            player.PickUp(ItemFactory.GenerateItem());
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultFont = Content.Load<SpriteFont>("Fonts/Arial");
            mainMenu.LoadContent(Content);

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.X + ScreenWidth/2, GraphicsDevice.Viewport.Y + ScreenHeight / 2);
            player.Initialize(Content.Load<Texture2D>("Sprites\\Player\\test.png"), playerPosition); 


            // This is the place to initialize all variables depending on external resources.
            textDrawer = new TextDrawer(defaultFont);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            textDrawer = new TextDrawer(defaultFont);
            currentKeyboardState =  Keyboard.GetState();
            mainMenu.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (GameState)
            {
                case GameState.MainMenu:mainMenu.Draw(spriteBatch);
                    break;
                case GameState.InGame: player.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
