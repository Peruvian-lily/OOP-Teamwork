namespace RPG.GameLogic.Models.Spells
{
    using RPG.GameLogic.Models.Characters;
    using Base;
    using Effects;
    using Stats;

    public class Heal : SelftCastSkill
    {
        /// <summary>
        /// Heal for full amount
        /// </summary>
        /// <param name="power">Strenght of heal</param>
        /// <param name="owner">Target of the Skill</param>
        public Heal(int power, Player owner) :
            base("Heal", new Health(power), owner)
        {
        }

        /// <summary>
        /// Heal for the amount and double that over the duration.
        /// </summary>
        /// <param name="power">Amount of health increased.</param>
        /// <param name="duration">Duration of heal effect.</param>
        /// <param name="owner">Target of the Skill.</param>
        public Heal(int power, int duration, Player owner)
            : base("Heal Over Time", new Health(power), new Healing((power * 2) / duration, duration), owner)
        {
        }

        public override void Cast()
        {
            Owner.Health.Increase(this.Stat.Value);
        }
    }
}
