namespace RPG.GameLogic.Models.Spells
{
    using System;
    using Base;
    using Effects;
    using Stats;
    using Characters.Base;
    using Effects.Base;
    using Stats.Base;

    class Heal : Spell
    {
        /// <summary>
        /// Heal for full amount
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="cost"></param>
        public Heal(Stat stat, int cost) : 
            base("Heal", stat, cost)
        {
        }

        /// <summary>
        /// Heal for a small amount at cast and then for full amount every tick.
        /// </summary>
        /// <param name="power">Amount of health increased</param>
        /// <param name="duration">Duration of heal effect</param>
        /// <param name="cost">Mana cost</param>
        public Heal(int power, int duration, int cost) 
            : base("Heal Over Time", new Health(power/5), 
            new Healing(power, duration), cost)
        {
        }

        public override void Cast(Character target)
        {
            if (!this.Effect.Equals(null))
            {
                this.Effect.Apply(target);
            }
            target.Health.Increase(this.Stat.Value);
        }
    }
}
