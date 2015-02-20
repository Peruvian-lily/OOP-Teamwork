using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG.Helpers
{
    class GUIElement
    {
        private Rectangle guiRectangle;

        public GUIElement(string assetName)
        {
            this.AssetName = assetName;
        }

        public delegate void ElementClicked(string element);

        public event ElementClicked ClickEvent;
        public Texture2D GUITextue { get; set; }
        public string AssetName { get; set; }

        public virtual void LoadContent(ContentManager content)
        {
            this.GUITextue = content.Load<Texture2D>(this.AssetName);
            this.guiRectangle = new Rectangle(0, 0, this.GUITextue.Width, this.GUITextue.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.GUITextue, this.guiRectangle, Color.White);
        }

        public virtual void Update()
        {
            if (this.guiRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this.ClickEvent(this.AssetName);
            }
        }

        public void CenterElement(int height, int width)
        {
            this.guiRectangle = new Rectangle((width / 2) - this.GUITextue.Width / 2, (height / 2) - this.GUITextue.Height / 2, this.GUITextue.Width, this.GUITextue.Height);
        }

        public void MoveElement(int x, int y)
        {
            this.guiRectangle = new Rectangle
                (this.guiRectangle.X += x, this.guiRectangle.Y += y, this.guiRectangle.Width, this.guiRectangle.Height
                );
        }


    }
}
