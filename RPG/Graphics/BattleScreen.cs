using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Core.Battle;
using RPG.GameLogic.Interface;
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
            //List<Character> enemies = ((Enemy)CollisionResult.enemy).GetAllies(worldObjects);
            this.battle = new Battle(player, worldObjects);

            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\background"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\left"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\right"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\select"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\player_start"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\enemy_start"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\futureui1"));
            this.battleScreen.Add(new GUIElement(@"Overlays\Battle\flee"));

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
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\left").MoveElement(-200, 50);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\right").MoveElement(200, 50);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\select").MoveElement(0, 50);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\player_start").MoveElement(-330, -100);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\enemy_start").MoveElement(330, -100);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\futureui1").MoveElement(0, 200);
            this.battleScreen.Find(x => x.AssetName == @"Overlays\Battle\flee").MoveElement(360, 120);
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
                    textDrawer.DrawString(spriteBatch, "Health: " + this.battle.Player.Health.Value, new Vector2(650, 420), Color.White);
                    textDrawer.DrawString(spriteBatch, this.battle.Player.Name, new Vector2(10, 5), Color.Indigo);
                    textDrawer.DrawString(spriteBatch, this.battle.PlayerState, new Vector2(335, 280), Color.Indigo);
                    break;
                case GameState.GamePlayState:
                    break;
            }
            textDrawer.DrawString(spriteBatch, this.battle.Status, new Vector2(50, 350), Color.WhiteSmoke);
        }

        public void OnClick(string element)
        {
            if (element == @"Overlays\Battle\flee")
            {
                //Return to Map
                Game1.CurrentState = GameState.GamePlayState;
            }
        }
    }
}
