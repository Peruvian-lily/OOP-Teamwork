using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Graphics
{
    public abstract class AbstractGameState
    {
        protected readonly Game game;


        protected AbstractGameState(Game game)
        {
            this.game = game;

        }
        public virtual void Update(GameTime gameTime)
        {
            // Update code common to every state.  
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw code common to every state.  
        }
    }  
}
