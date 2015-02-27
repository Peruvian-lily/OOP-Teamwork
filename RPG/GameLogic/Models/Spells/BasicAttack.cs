using RPG.GameLogic.Interface;

namespace RPG.GameLogic.Models.Spells
{
    using Characters;
    using Characters.Base;
    using Base;
    using Stats.Base;

    class BasicAttack : TargetedSkill
    {
        public BasicAttack(Player owner) 
            : base("Basic Attack", owner.AttackPower, owner)
        {
        }

        public BasicAttack(string name, Stat stat, Effects.Base.Effects effect, Player owner) 
            : base(name, stat, effect, owner)
        {
        }

        public override void Cast(Character target)
        {
            this.Owner.Attack(target as IFight);
        }
    }
}
