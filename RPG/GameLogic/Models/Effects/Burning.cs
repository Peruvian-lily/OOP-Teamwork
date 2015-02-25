using System.Collections.Generic;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Effects.Base;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Effects
{
    class Burning : Base.Effects
    {
        public Burning(int value, int duration) 
            : base(new Health(value), duration, EffectType.Harmful)
        {
        }

        public override void Tick(Character target)
        {
            if (this.Duration <= 0) return;
            this.Duration -= 1;
            target.TakeDamage(this.Stat.Value, new List<Stat> { this.Stat });
        }
    }
}
