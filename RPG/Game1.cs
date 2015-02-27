using RPG.GameLogic.Enums;

namespace RPG
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using RPG.GameLogic.Core.Factory;
    using RPG.GameLogic.Models.Characters;
    using RPG.GameLogic.Models.Characters.Base;
    using RPG.GameLogic.Models.Spells;
    using RPG.Graphics;
    using RPG.Graphics.GameStates;

    public class Game1 : Game
    {
        public const int BufferWidth = 800;
        public const int BufferHeight = 480;

        public static GameState CurrentState;
        public new static ContentManager Content;
        public static int ScreenWidth;
        public static int ScreenHeight;
        private Player player;
        private AbstractGameState[] States;
        private GraphicsDeviceManager graphics;
        private List<Character> worldObjects;
        private SpriteBatch spriteBatch;

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
            this.graphics.ApplyChanges();
            ScreenWidth = this.GraphicsDevice.Viewport.Width;
            ScreenHeight = this.GraphicsDevice.Viewport.Height;
            this.IsMouseVisible = true;
            base.Content = Content;
            this.worldObjects = new List<Character>();
            string name = "QueBabche";
            int attack = 100;
            int defense = 100;
            int health = 100;
            int inventorySize = 5;
            this.player = new Player(name, health, attack, defense, inventorySize);
            this.player.Position = new Vector2((float)ScreenWidth / 2, (float)ScreenHeight / 2);
            this.player.PickUp(ItemFactory.GenerateItem(25, 50));
            this.player.PickUp(ItemFactory.GenerateItem(25, 50));
            this.player.PickUp(ItemFactory.GenerateItem(25, 50));
            this.player.LearnSkill(new Fireball(30, 2, this.player));
            this.player.LearnSkill(new Heal(50, 5, this.player));
            this.player.LearnSkill(new BasicAttack(this.player));

            // Use reflection to determine how many states there are as  
            // .NET Compact Framework does not support Enum.GetValues().  
            Type type = typeof(GameState);
            FieldInfo[] info = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            Int32 numberStates = info.Length;

            // Instantiate each Game state.  
            this.States = new AbstractGameState[numberStates];
            this.States[(Int32)GameState.MainMenuState] = new MainMenuState(this);
            this.States[(Int32)GameState.GamePlayState] = new GamePlayState(this, this.player, this.worldObjects);
            this.States[(Int32)GameState.BattleScreenState] = new BattleScreenState(this, this.player, this.worldObjects);

            // Initialize current Game state.  
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
            this.States[(Int32)CurrentState].Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.States[(Int32)CurrentState].Draw(this.spriteBatch);
            base.Draw(gameTime);
        }
    }
}
