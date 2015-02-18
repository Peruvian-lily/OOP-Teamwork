using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Helpers
{
    class Animation
    {
        private Texture2D animation;
        private Rectangle sourceRectangle;

        private float elapsed;
        private float frameTime;
        private int numOfFrames;
        private int currentFrame;
        private int width;
        private int height;
        private int frameWidth;
        private int frameHeight;
        private bool looping;

        public Animation(string assetName, float frameSpeed, int numOfFrames, bool looping, int initialX, int initialY)
        {
            this.frameTime = frameSpeed;
            this.numOfFrames = numOfFrames;
            this.looping = looping;
            this.animation = Game1.Content.Load<Texture2D>(assetName);
            this.frameWidth = (this.animation.Width / numOfFrames);
            this.frameHeight = (this.animation.Height/4+1);
            this.Position = new Vector2(initialX, initialY);
        }

        public Vector2 Position { get; set; }

        public void PlayAnimation(GameTime gameTime)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.sourceRectangle = new Rectangle(this.currentFrame *this.frameWidth, this.frameHeight, this.frameWidth, this.frameHeight);
            if (this.elapsed >= this.frameTime)
            {
                if (this.currentFrame >= this.numOfFrames - 1)
                {
                    if (this.looping)
                    {
                        this.currentFrame = 0;
                    }
                }
                else
                {
                    this.currentFrame++;
                }
                this.elapsed = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.animation, this.Position, this.sourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
        }
    }
}
