using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Core.Battle;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;

namespace RPG.Graphics
{
    class BattleScreen
    {
        private Battle battle;
        private TextDrawer textDrawer;
        private SpriteFont defaultFont;
        List<GUIElement> battleScreen = new List<GUIElement>();
        public BattleScreen(Player player, List<Character> worldObjects)
        {
            this.battle = new Battle(player, worldObjects);

            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\background"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\button1"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\button2"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\left"));

            this.defaultFont = Game1.Content.Load<SpriteFont>(@"Fonts\Arial");
            this.textDrawer = new TextDrawer(this.defaultFont);

            battle.StartFight();
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GUIElement element in this.battleScreen)
            {
                element.LoadContent(content);
                element.CenterElement(400, 800);
                element.ClickEvent += this.OnClick;
            }
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\button1").MoveElement(-300, 0);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\button2").MoveElement(300, 0);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\left").MoveElement(-200, 200);
        }

        public void Update()
        {
            switch (Game1.CurrentState)
            {
                case GameState.BattleScreenState:
                    foreach (GUIElement element in this.battleScreen)
                    {
                        element.Update();
                    }
                    break;
                case GameState.GamePlayState:
                    break;
            }
            if (battle.InProgress)
            {
                battle.NextTurn();
            }
            else
            {
                Game1.CurrentState = GameState.GamePlayState;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (Game1.CurrentState)
            {
                case GameState.BattleScreenState:
                    foreach (GUIElement element in this.battleScreen)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.GamePlayState:
                    break;
            }
            string statusText = this.battle.Status;
            //String.Format("Attacker: {0}/{1}hp; Target: {2}/{3}hp",
            //(this.battle.Attacker as Character).Name, this.battle.Attacker.Health.Value,
            //(this.battle.Target as Character).Name, this.battle.Target.Health.Value);
            textDrawer.DrawString(spriteBatch, statusText);
        }

        public void OnClick(string element)
        {
            if (element == @"Overlays\Battle\button1")
            {
                //Play the Game
                Game1.CurrentState = GameState.GamePlayState;
            }
            if (element == @"Overlays\Battle\button2")
            {
                
            }
        }
    }
}
