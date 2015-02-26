namespace RPG.GameLogic.Models.Spells
{
    using Base;
    using Effects;
    using Stats;
    using Characters.Base;
    using Stats.Base;

    public class Heal : Spell
    {
        /// <summary>
        /// Heal for full amount
        /// </summary>
        /// <param name="power">Strenght of heal</param>
        /// <param name="cost">Cost of spell</param>
        public Heal(int power, int cost) : 
            base("Heal", new Health(power), cost)
        {
        }

        /// <summary>
        /// Heal for a small amount at cast and then for full amount every tick.
        /// </summary>
        /// <param name="power">Amount of health increased</param>
        /// <param name="duration">Duration of heal effect</param>
        /// <param name="cost">Cost of spell</param>
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
