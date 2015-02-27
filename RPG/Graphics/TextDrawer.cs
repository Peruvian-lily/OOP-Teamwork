namespace RPG.Graphics
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This class provides abstraction for the SpriteDrawer DrawString functionality of XNA.
    /// </summary>
    public class TextDrawer
    {
        private SpriteFont font;
        private float rotation;
        private Vector2 originOfRotation;

        public TextDrawer(SpriteFont font, float rotationAngle, Vector2 originOfRotation) 
        {
            this.Font = font;
            this.Rotation = rotationAngle;
            this.OriginOfRotation = originOfRotation;
        }
        public TextDrawer(SpriteFont font) 
            : this(font, 0f, new Vector2()) 
        { 
        }

        public SpriteFont Font
        {
            get
            {
                return this.font;
            }

            private set 
            {
                this.font = value;
            }
        }

        public float Rotation 
        {
            get
            {
                return this.rotation;
            }

            set 
            {
                this.rotation = value;
            }
        }

        public Vector2 OriginOfRotation 
        {
            get
            {
                return this.originOfRotation;
            }

            set 
            {
                this.originOfRotation = value;
            }
        }


        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position = new Vector2(), Color? color = null)
        {
            if (color == null)
            {
                color = Color.Black;
            }

            spriteBatch.DrawString(this.Font, text, position, (Color)color);
        }

        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color color, Vector2 scale)
        {
            spriteBatch.DrawString(this.Font, text, position, color, this.Rotation, this.OriginOfRotation, scale, SpriteEffects.None, 0);
        }
    }
}
