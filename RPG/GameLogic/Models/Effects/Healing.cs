namespace RPG.GameLogic.Models.Effects
{
    using RPG.GameLogic.Models.Characters.Base;
    using RPG.GameLogic.Models.Effects.Base;
    using RPG.GameLogic.Models.Stats;

    class Healing : Base.Effects
    {
        public Healing(int amount, int duration)
            : base(new Health(amount), duration, EffectType.Helpful)
        {
        }

        public override void Tick(Character target)
        {
            if (this.Duration <= 0)
            {
                return;
            }

            this.Duration -= 1;
            target.Health.Increase(this.Stat.Value);
        }
    }
}
