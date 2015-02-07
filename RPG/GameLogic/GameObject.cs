using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.GameLogic
{
    public abstract class GameObject
    {
        private string _id;

        public Vector2 Position;
        public Texture2D Texture;

        public string Id
        {
            get { return this._id; }
            private set
            {
                if (value == null || value.Length < 3)
                {
                    throw new ArgumentOutOfRangeException("Id must be longer than 3 symbols");
                }
                this._id = value;
            }
        }
        protected GameObject(string id)
        {
            this.Id = id;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
