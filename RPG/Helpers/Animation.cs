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

        public Animation(ContentManager content, string assetName, float frameSpeed, int numOfFrames, bool looping)
        {
            this.frameTime = frameSpeed;
            this.numOfFrames = numOfFrames;
            this.looping = looping;
            this.animation = content.Load<Texture2D>(assetName);
            this.frameWidth = (animation.Width / numOfFrames);
            this.frameHeight = (animation.Height);
            this.Position = new Vector2(100, 100);
        }

        public Vector2 Position { get; set; }

        public void PlayAnimation(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            if (elapsed >= frameTime)
            {
                if (currentFrame >= numOfFrames - 1)
                {
                    if (looping)
                    {
                        currentFrame = 0;
                    }
                }
                else
                {
                    currentFrame++;
                }
                elapsed = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation, this.Position, sourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
        }
    }
}
