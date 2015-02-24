#region Using Statements

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
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
using RPG.Graphics.GameStates;
using RPG.Graphics.Map;

#endregion

namespace RPG
{
    public class Game1 : Game
    {
        public static GameState CurrentState;
        private AbstractGameState[] States;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public new static ContentManager Content;
        public static int ScreenWidth;
        public static int ScreenHeight;
        private Player player;
        private List<Character> worldObjects;

        public Game1()
            : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
            Content = base.Content;
        }

        protected override void Initialize()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();
            ScreenWidth = this.GraphicsDevice.Viewport.Width;
            ScreenHeight = this.GraphicsDevice.Viewport.Height;

            base.Content = Content;
            this.worldObjects = new List<Character>();
            this.player = new Player("QueBabche", 100, 100, 100, 5);
            this.player.Position = new Vector2((float)ScreenWidth / 2, (float)ScreenHeight / 2);
            this.player.PickUp(ItemFactory.GenerateItem(25, 50));

            // Use reflection to determine how many states there are as  
            // .NET Compact Framework does not support Enum.GetValues().  
            Type type = typeof(GameState);
            FieldInfo[] info = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            Int32 numberStates = info.Length;

            // Instantiate each game state.  
            States = new AbstractGameState[numberStates];
            States[(Int32)GameState.MainMenuState] = new MainMenuState(this);
            States[(Int32)GameState.GamePlayState] = new GamePlayState(this, this.player, this.worldObjects);
            States[(Int32)GameState.BattleScreenState] = new BattleScreenState(this, this.player, this.worldObjects);

            // Initialize current game state.  
            CurrentState = GameState.MainMenuState; 
            

            base.Initialize();

        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            /*
                textDrawer = new TextDrawer(defaultFont);
            */

            States[(Int32)CurrentState].Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            States[(Int32)CurrentState].Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
