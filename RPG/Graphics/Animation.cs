namespace RPG.Graphics
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        public readonly int FrameWidth;
        public readonly int FrameHeight;

        private const int NumOfRows = 4;
        
        private float elapsed;
        private float frameTime;
        private int numOfFrames;
        private int currentFrame;
        private bool looping;
        private Texture2D animation;
        private Rectangle sourceRectangle;

        public Animation(string assetName, float frameSpeed, int numOfFrames, byte wantedFigure, bool looping, float initialX, float initialY)
        {
            this.frameTime = frameSpeed;
            this.numOfFrames = numOfFrames;
            this.looping = looping;
            this.animation = Game1.Content.Load<Texture2D>(assetName);
            this.FrameWidth = this.animation.Width / numOfFrames;
            this.FrameHeight = this.animation.Height / NumOfRows;
            this.Position = new Vector2(initialX, initialY);
            this.WantedFigure = wantedFigure;
        }
        
        public Vector2 Position { get; set; }

        public Texture2D animTexture { get { return this.animation; } }

        public byte WantedFigure { get; set; }    
      
        public void PlayAnimation(GameTime gameTime)
        {
            int row = 4 - this.WantedFigure;
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.sourceRectangle = new Rectangle(this.currentFrame * this.FrameWidth, this.FrameHeight * row, this.FrameWidth, this.FrameHeight);
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
        public void Draw(SpriteBatch spriteBatch, Vector2 playerPosition)
        {
            Vector2 currentPosition = new Vector2(this.Position.X - playerPosition.X, this.Position.Y - playerPosition.Y);
            spriteBatch.Draw(this.animation, this.Position, this.sourceRectangle, Color.White, 0f, currentPosition, 1f, SpriteEffects.None, 1f);
        }
    }
}
