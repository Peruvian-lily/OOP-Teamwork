using System;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Spells.Base
{
    using Effects.Base;
    public abstract class Spell
    {
        protected Spell(String name, Stat stat, int cost)
            :this(name, stat, null, cost)
        {
        }
        protected Spell(String name, Stat stat, Effects effect, int cost)
        {
            this.Name = name;
            this.Stat = stat;
            this.Cost = cost;
            this.Effect = effect;
        }

        public String Name { get; set; }
        public Stat Stat { get; set; }
        public Effects Effect { get; set; }
        public int Cost { get; set; }

        public abstract void Cast(Character target);
    }
}
