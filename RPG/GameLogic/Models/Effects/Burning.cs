using RPG.GameLogic.Models.Stats;

namespace RPG.GameLogic.Models.Effects
{
    using Interface;
    using Base;

    class Burning : Effects
    {
        public Burning(int value, EffectTarget target, IFight owner) 
            : base(new Health(value), target, EffectType.Harmful, owner)
        {
        }

        public override void Apply()
        {
            if (this.Owner != null)
            {
                this.Owner.Health.Reduce(this.Stat.Value);
            }
        }
    }
}
