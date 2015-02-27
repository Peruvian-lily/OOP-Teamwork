using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Spells.Base
{
    public abstract class TargetedSkill : Skill
    {
        protected TargetedSkill(string name, Stat stat, Player owner) 
            : base(name, stat, owner)
        {
        }

        protected TargetedSkill(string name, Stat stat, Effects.Base.Effects effect, Player owner) 
            : base(name, stat, effect, owner)
        {
        }

        public abstract void Cast(Character target);
    }
}
