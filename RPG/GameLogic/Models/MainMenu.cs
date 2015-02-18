using System;
using System.Collections.Generic;
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
            main.Add(new GUIElement("Overlays\\Menu\\background"));
            main.Add(new GUIElement("Overlays\\Menu\\new_game_plain"));
            main.Add(new GUIElement("Overlays\\Menu\\exit_plain"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(400, 800);
                element.ClickEvent += OnClick;
            }
            main.Find(x => x.AssetName == "Overlays\\Menu\\new_game_plain").MoveElement(0, -100);
            main.Find(x => x.AssetName == "Overlays\\Menu\\exit_plain").MoveElement(0, 100);
        }

        public void Update()
        {
            switch (Game1.GameState)
            {
                case GameState.MainMenu:
                    foreach (GUIElement element in main)
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
                    foreach (GUIElement element in main)
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
