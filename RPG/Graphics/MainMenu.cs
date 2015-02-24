using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.Graphics
{
    class MainMenu
    {
        List<GUIElement> main = new List<GUIElement>();

        public MainMenu()
        {
            this.main.Add(new GUIElement("Overlays\\Menu\\background"));
            this.main.Add(new GUIElement("Overlays\\Menu\\new_game_plain"));
            this.main.Add(new GUIElement("Overlays\\Menu\\exit_plain"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in this.main)
            {
                element.LoadContent(content);
                element.CenterElement(400, 800);
                element.ClickEvent += this.OnClick;
            }
            this.main.Find(x => x.AssetName == "Overlays\\Menu\\new_game_plain").MoveElement(0, -100);
            this.main.Find(x => x.AssetName == "Overlays\\Menu\\exit_plain").MoveElement(0, 100);
        }

        public void Update()
        {
            switch (Game1.CurrentState)
            {
                case GameState.MainMenuState:
                    foreach (GUIElement element in this.main)
                    {
                        element.Update();
                    }
                    break;
                case GameState.GamePlayState:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (Game1.CurrentState)
            {
                case GameState.MainMenuState:
                    foreach (GUIElement element in this.main)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.GamePlayState:
                    break;
            }
        }

        public void OnClick(string element)
        {
            if (element == "Overlays\\Menu\\new_game_plain")
            {
                //Play the Game
                Game1.CurrentState = GameState.GamePlayState;
            }
        }
    }
}
