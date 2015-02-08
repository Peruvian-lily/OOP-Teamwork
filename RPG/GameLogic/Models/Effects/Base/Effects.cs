namespace RPG.GameLogic.Models.Effects.Base
{
    using Interface;
    using NPC.Base;
    using Stats.Base;

    public abstract class Effects : ITimeout
    {
        /// <summary>
        /// Create new status effect.
        /// </summary>
        /// <param name="stat">Stat it modifies</param>
        /// <param name="target">Target it affects</param>
        /// <param name="duration">Default value: 1 turn.</param>
        /// <param name="type">Is the effect helpfull or hafmul</param>
        protected Effects(Stat stat, Npc target, int duration, EffectType type)
        {
            this.Stat = stat;
            this.Target = target;
            this.Duration = duration;
        }
        protected Effects(Stat stat, Npc target, EffectType type)
            : this(stat, target, 1, type)
        {
            
        }

        public Stat Stat { get; private set; }
        public Npc Target { get; private set; }
        public int Duration { get; private set; }

        public abstract void Apply();
    }
}
