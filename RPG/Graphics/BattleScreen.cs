using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG.Helpers;

namespace RPG.GameLogic.Models
{
    class BattleScreen
    {
        List<GUIElement> battleScreen = new List<GUIElement>();
        public BattleScreen()
        {
            this.battleScreen.Add(new GUIElement("Overlays\\Battle\\background"));
            this.battleScreen.Add(new GUIElement("Overlays\\Battle\\button1"));
            this.battleScreen.Add(new GUIElement("Overlays\\Battle\\button2"));
            this.battleScreen.Add(new GUIElement("Overlays\\Battle\\left"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in this.battleScreen)
            {
                element.LoadContent(content);
                element.CenterElement(400, 800);
                element.ClickEvent += this.OnClick;
            }
            this.battleScreen.Find(x => x.AssetName == "Overlays\\Battle\\button1").MoveElement(-300,0);
            this.battleScreen.Find(x => x.AssetName == "Overlays\\Battle\\button2").MoveElement(300, 0);
            this.battleScreen.Find(x => x.AssetName == "Overlays\\Battle\\left").MoveElement(-200, 200);
        }

        public void Update()
        {
            switch (Game1.GameState)
            {
                case GameState.Battle:
                    foreach (GUIElement element in this.battleScreen)
                    {
                        element.Update();
                    }
                    break;
                case GameState.InGame:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (Game1.GameState)
            {
                case GameState.Battle:
                    foreach (GUIElement element in this.battleScreen)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.InGame:
                    break;
            }
        }

        public void OnClick(string element)
        {
            if (element == "Overlays\\Battle\\button1")
            {
                //Play the Game
                Game1.GameState = GameState.InGame;
            }
        }
    }
}
