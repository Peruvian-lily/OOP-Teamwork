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
            this.battleScreen.Add(new GUIElement("Overlays\\Menu\\background"));
            this.battleScreen.Add(new GUIElement("Overlays\\Menu\\new_game_plain"));
            this.battleScreen.Add(new GUIElement("Overlays\\Menu\\exit_plain"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in this.battleScreen)
            {
                element.LoadContent(content);
                element.CenterElement(400, 800);
                element.ClickEvent += this.OnClick;
            }
            this.battleScreen.Find(x => x.AssetName == "Overlays\\Menu\\new_game_plain").MoveElement(0, -100);
            this.battleScreen.Find(x => x.AssetName == "Overlays\\Menu\\exit_plain").MoveElement(0, 100);
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
                case GameState.MainMenu:
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
            if (element == "Overlays\\Menu\\new_game_plain")
            {
                //Play the Game
                Game1.GameState = GameState.InGame;
            }
        }
    }
}
