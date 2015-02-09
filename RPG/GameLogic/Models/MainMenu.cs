﻿using System;
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
        GameState gameState = new GameState();

        public MainMenu()
        {
            main.Add(new GUIElement("Overlays\\Menu\\main_menu_background_under_buttons"));
            main.Add(new GUIElement("Overlays\\Menu\\new_game_plain"));
            main.Add(new GUIElement("Overlays\\Menu\\exit_plain"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.ScreenHeight, Game1.ScreenWidth);
                element.ClickEvent += OnClick;
            }
            main.Find(x => x.AssetName == "Overlays\\Menu\\new_game_plain").MoveElement(0, -100);
            main.Find(x => x.AssetName == "Overlays\\Menu\\exit_plain").MoveElement(0, 100);
        }

        public void Update()
        {
            foreach (GUIElement element in main)
            {
                element.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GUIElement element in main)
            {
                element.Draw(spriteBatch);
            }
        }

        public void OnClick(string element)
        {
            if (element == "Overlays\\Menu\\new_game_plain")
            {
                //Play the Game
                gameState = GameState.InGame;
            }
        }
    }
}