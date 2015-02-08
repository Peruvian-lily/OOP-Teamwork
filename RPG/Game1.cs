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
using RPG.GameLogic.Models.NPC.Base;
using RPG.GameLogic.Core;
using RPG.Helpers;

#endregion

namespace RPG
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState currentKeyboardState;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private Engine engine;
        private Player player;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;

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

            engine = Engine.GetInstance;
            player = new Player("placeholder", "placeholder",100,100,100,5);
            player.PickUp(ItemFactory.GenerateItem());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultFont = Content.Load<SpriteFont>("Fonts/Arial");

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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            textDrawer.DrawString(spriteBatch, ItemFactory.GenerateItem().Name, new Vector2(10, 10), Color.Black);
            textDrawer.DrawString(spriteBatch, "More text in Arial with scale 2", new Vector2(30, 30), Color.Black, new Vector2(2, 2));

            base.Draw(gameTime);
        }
    }
}
