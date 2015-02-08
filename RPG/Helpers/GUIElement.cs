using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Helpers
{
    class GUIElement
    {

        public GUIElement(string assetName)
        {
            this.AssetName = assetName;
        }

        public Texture2D GUITextue { get; set; }
        public Rectangle GUIRectangle { get; set; }
        public string AssetName { get; set; }

        public void LoadContent(ContentManager content)
        {
            GUITextue = content.Load<Texture2D>(AssetName);
            GUIRectangle = new Rectangle(0,0,GUITextue.Width,GUITextue.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITextue, GUIRectangle, Color.White);
        }

        public void CenterElement(int height, int width)
        {
            GUIRectangle = new Rectangle((width/2) - this.GUITextue.Width/2,(height/2)-this.GUITextue.Height/2,this.GUITextue.Width,this.GUITextue.Height);
        }
    }
}
