namespace RPG.Graphics.GameStates
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class MainMenuState : AbstractGameState
    {
        public MainMenuState(Game game)
            : base(game)
        {
            this.Menu = new MainMenu();
            this.Menu.LoadContent(Game1.Content);
        }

        public MainMenu Menu { get; private set; }

        public override void Update(GameTime gameTime)
        {
            this.Menu.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            this.Menu.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
