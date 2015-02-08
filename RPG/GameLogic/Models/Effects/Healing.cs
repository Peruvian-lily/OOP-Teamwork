namespace RPG.GameLogic.Models.Effects
{
    using Interface;
    using Stats;
    using Base;

    class Healing : Effects
    {
        public Healing(int amount, EffectTarget target, IFight owner) 
            : base(new Health(amount), target, EffectType.Helpful, owner)
        {
        }

        public override void Apply()
        {
            if (Owner != null)
            {
                switch (this.EffectTarget)
                {
                    case EffectTarget.Self:
                        Owner.Health.Increase(this.Stat.Value);
                        break;
                    case EffectTarget.Others:
                        Owner.GetTarget().Health.Increase(this.Stat.Value);
                        break;
                }
            }
        }
    }
}
