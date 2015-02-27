using RPG.GameLogic.Models.Characters;

namespace RPG.GameLogic.Models.Spells
{
    using System.Collections.Generic;
    using Stats.Base;
    using Characters.Base;
    using Effects;
    using Base;
    using Stats;

    public class Fireball : TargetedSkill
    {
        /// <summary>
        /// Create a fireball that deals damage and a third of that damage over the duration.
        /// </summary>
        /// <param name="power">Strenght of Skill</param>
        /// <param name="cost">Mana cost</param>
        /// <param name="duration">Duration of burn effect in turns</param>
        public Fireball(int power, int duration, Player owner) :
            base("Fireball", new Fire(power), 
            new Burning(power / 3, duration), owner)
        {
        }

        public override void Cast(Character target)
        {
            target.TakeDamage(this.Stat.Value, new List<Stat> { this.Stat });
            this.Effect.Apply(target);
        }
    }
}
