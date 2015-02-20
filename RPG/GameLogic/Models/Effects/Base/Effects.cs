namespace RPG.GameLogic.Models.Effects.Base
{
    using Interface;
    using Stats.Base;

    public abstract class Effects
    {
        /// <summary>
        /// Create new status effect.
        /// </summary>
        /// <param name="stat">Stat it modifies</param>
        /// <param name="target">Target it affects</param>
        /// <param name="type">Is the effect helpfull or hafmul</param>
        protected Effects(Stat stat, EffectTarget target, EffectType type, IFight owner)
        {
            this.Stat = stat;
            this.EffectTarget = target;
            this.EffectType = type;
            this.Owner = owner;
        }

        public IFight Owner { get; private set; }
        public Stat Stat { get; private set; }
        public EffectType EffectType { get; private set; }
        public EffectTarget EffectTarget { get; private set; } 

        public abstract void Apply();
    }
}
