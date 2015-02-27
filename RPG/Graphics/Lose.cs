using Microsoft.Xna.Framework;
using RPG.GameLogic.Enums;

namespace RPG.Graphics
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

   public class Lose
    {
        List<GUIElement> lose = new List<GUIElement>();

        public Lose()
        {
            this.lose.Add(new GUIElement("Overlays\\Menu\\dead"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in this.lose)
            {
                element.LoadContent(content);
                element.CenterElement(400, 800);
                element.ClickEvent += this.OnClick;
            }
        }

        public void Update()
        {
            switch (Game1.CurrentState)
            {
                case GameState.LoseState:
                    foreach (GUIElement element in this.lose)
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
                case GameState.LoseState:
                    foreach (GUIElement element in this.lose)
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
            if (element == "Overlays\\Menu\\dead")
            {
                //Go to Main
                Game1.CurrentState = GameState.MainMenuState;
            }
        }
    }
}
