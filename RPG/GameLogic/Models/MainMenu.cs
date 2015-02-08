using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG.Helpers;

namespace RPG.GameLogic.Models
{
    class MainMenu
    {
        List<GUIElement> main = new List<GUIElement>();


        public MainMenu()
        {
            main.Add(new GUIElement("Overlays\\Menu\\main_menu_background_under_buttons"));
          main.Add(new GUIElement("Overlays\\Menu\\new_game_plain"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.ScreenHeight,Game1.ScreenWidth);
            }
           main.Find(x => x.AssetName == "Overlays\\Menu\\new_game_plain").MoveElement(0,-100);
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GUIElement element in main)
            {
                element.Draw(spriteBatch);
            }
        }
    }
}
