using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Core.Battle;
using RPG.GameLogic.Models;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;

namespace RPG.Graphics.GameStates
{
    class BattleScreenState:AbstractGameState
    {
        private BattleScreen battleScreen;
        private Battle battle;
        private SpriteFont defaultFont;
        private TextDrawer textDrawer;

        public BattleScreenState(Game game, Player player, List<Character> worldObjects) : base(game)
        {
            this.battleScreen = new BattleScreen();
            this.battleScreen.LoadContent(Game1.Content);
            this.battle = new Battle(player, worldObjects);
            battle.StartFight();
            this.defaultFont = Game1.Content.Load<SpriteFont>("Fonts\\Arial");
            this.textDrawer = new TextDrawer(this.defaultFont);
        }

        public override void Update(GameTime gameTime)
        {
            if (battle.InProgress)
            {
                battle.NextTurn();
            }
            else
            {
                Game1.CurrentState = GameState.GamePlayState;
            }
            this.battleScreen.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            this.battleScreen.Draw(spriteBatch);
            string statusText = this.battle.Status;
            //String.Format("Attacker: {0}/{1}hp; Target: {2}/{3}hp",
            //(this.battle.Attacker as Character).Name, this.battle.Attacker.Health.Value,
            //(this.battle.Target as Character).Name, this.battle.Target.Health.Value);
            textDrawer.DrawString(spriteBatch, statusText);
            spriteBatch.End();
        }
    }
}
