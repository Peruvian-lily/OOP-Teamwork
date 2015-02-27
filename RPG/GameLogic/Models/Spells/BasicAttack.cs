namespace RPG.GameLogic.Models.Spells
{
    using Base;
    using Characters;
    using Characters.Base;
    using RPG.GameLogic.Interface;
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
