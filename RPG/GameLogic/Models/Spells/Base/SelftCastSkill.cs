namespace RPG.GameLogic.Models.Spells.Base
{
    using RPG.GameLogic.Models.Characters;
    using RPG.GameLogic.Models.Stats.Base;

    public abstract class SelftCastSkill : Skill
    {
        protected SelftCastSkill(string name, Stat stat, Player owner) 
            : base(name, stat, owner)
        {
        }

        protected SelftCastSkill(string name, Stat stat, Effects.Base.Effects effect, Player owner) 
            : base(name, stat, effect, owner)
        {
        }

        public abstract void Cast();
    }
}
