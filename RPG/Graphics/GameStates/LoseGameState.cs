using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Graphics.GameStates
{
    class LoseGameState:AbstractGameState
    {
        public LoseGameState(Game game)
            : base(game)
        {
            this.Menu = new Lose();
            this.Menu.LoadContent(Game1.Content);
        }

        public Lose Menu { get; private set; }

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
    
