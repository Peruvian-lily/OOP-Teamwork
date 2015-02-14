using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace RPG.Helpers
{

    /// <summary>
    /// This class provides abstraction for the SpriteDrawer DrawString functionality of XNA.
    /// </summary>
    public class TextDrawer
    {
        private SpriteFont _font;
        private float _rotation;
        private Vector2 _originOfRotation;

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
            get { return this._font; }
            private set 
            {
                this._font = value;
            }
        }

        public float Rotation 
        {
            get { return this._rotation; }
            set 
            {
                this._rotation = value;
            }
        }

        public Vector2 OriginOfRotation 
        {
            get { return this._originOfRotation; }
            set 
            {
                this._originOfRotation = value;
            }
        }

        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(this.Font, text, position, color);
            spriteBatch.End();
        }
        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color color, Vector2 scale)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(this.Font, text, position, color, this.Rotation, 
                OriginOfRotation, scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
