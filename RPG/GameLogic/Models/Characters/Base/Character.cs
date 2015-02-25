using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Characters.Base
{
    using Effects.Base;

    public abstract class Character : GameObject
    {
        private string name;

        protected Character(string name, int health, int defense)
        {
            this.Name = name;
            this.Health = new Health(health);
            this.Defense = new Defense(defense);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (value == null || value.Length < 2)
                {
                    throw new ArgumentOutOfRangeException("Name must be 2 or more symbols long.");
                }
                this.name = value;
            }
        }

        public Health Health { get; private set; }

        public Defense Defense { get; private set; }

        public List<Effects> Effects { get; set; }

        public int Width
        {
            get
            {
                // In the sprite, we are using, the character is drawn 3 times 
                // on the X axis so to get his width he divide by 3!
                const int SPRITE_WIDTH_OFFSET = 3;
                return this.Texture.Width / SPRITE_WIDTH_OFFSET;
            }
        }

        public int Height
        {
            get
            {
                // In the sprite, we are using, the character is drawn 4 times 
                // on the Y axis so to get his height he divide by 4!
                const int SPRITE_HEIGHT_OFFSET = 4;
                return this.Texture.Height / SPRITE_HEIGHT_OFFSET;
            }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void TakeDamage(int amount, List<Stat> types);
    }
}
