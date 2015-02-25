namespace RPG.GameLogic.Models.Spells
{
    using System.Collections.Generic;
    using Stats.Base;
    using Characters.Base;
    using Effects;
    using Effects.Base;
    using Base;
    using Stats;

    class Fireball : Spell
    {
        /// <summary>
        /// Create a fireball that deals damage and a third of that damage over the duration.
        /// </summary>
        /// <param name="power">Strenght of spell</param>
        /// <param name="cost">Mana cost</param>
        /// <param name="duration">Duration of burn effect in turns</param>
        public Fireball(int power,int duration, int cost) :
            base("Fireball", new Fire(power), 
            new Burning(power / 3, duration), cost)
        {
        }

        public override void Cast(Character target)
        {
            target.TakeDamage(this.Stat.Value, new List<Stat> { this.Stat });
            this.Effect.Apply(target);
        }
    }
}
