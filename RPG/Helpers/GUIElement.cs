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

        public void LoadContent(ContentManager content)
        {
            GUITextue = content.Load<Texture2D>(AssetName);
            guiRectangle = new Rectangle(0, 0, GUITextue.Width, GUITextue.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITextue, guiRectangle, Color.White);
        }

        public void Update()
        {
            if (guiRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ClickEvent(AssetName);
            }
        }

        public void CenterElement(int height, int width)
        {
            guiRectangle = new Rectangle((width / 2) - this.GUITextue.Width / 2, (height / 2) - this.GUITextue.Height / 2, this.GUITextue.Width, this.GUITextue.Height);
        }

        public void MoveElement(int x, int y)
        {
            guiRectangle = new Rectangle(guiRectangle.X += x, guiRectangle.Y += y,guiRectangle.Width,guiRectangle.Height);
        }


    }
}
