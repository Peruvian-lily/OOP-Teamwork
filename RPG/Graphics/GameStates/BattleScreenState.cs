namespace RPG.Graphics.GameStates
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using RPG.GameLogic.Core.Battle;
    using RPG.GameLogic.Models.Characters;
    using RPG.GameLogic.Models.Characters.Base;

    class BattleScreenState : AbstractGameState
    {
        private BattleScreen battleScreen;
        private Battle battle;

        public BattleScreenState(Game game, Player player, List<Character> worldObjects) : base(game)
        {
            this.battleScreen = new BattleScreen(player, worldObjects);
            this.battleScreen.LoadContent(Game1.Content);
        }

        public override void Update(GameTime gameTime)
        {
            this.battleScreen.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            this.battleScreen.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
