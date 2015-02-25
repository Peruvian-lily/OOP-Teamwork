namespace RPG.GameLogic.Models.Effects.Base
{
    using Stats.Base;
    using Characters.Base;

    public abstract class Effects
    {
        protected Effects(Stat stat, int duration, EffectType type)
        {
            this.Stat = stat;
            this.EffectType = type;
            this.Duration = duration;
        }

        public Stat Stat { get; private set; }
        public EffectType EffectType { get; private set; }
        public int Duration { get; set; }

        public virtual void Apply(Character target)
        {
            target.Effects.Add(this);
        }

        public abstract void Tick(Character target);
    }
}
