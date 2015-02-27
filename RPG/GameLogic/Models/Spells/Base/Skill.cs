using System;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Spells.Base
{
    using Effects.Base;
    public abstract class Skill
    {
        protected Skill(String name, Stat stat, Player owner)
            :this(name, stat, null, owner)
        {
        }
        protected Skill(String name, Stat stat, Effects effect, Player owner)
        {
            this.Name = name;
            this.Stat = stat;
            this.Effect = effect;
            this.Owner = owner;
        }

        public Player Owner { get; set; }
        public String Name { get; set; }
        public Stat Stat { get; set; }
        public Effects Effect { get; set; }
        public int Cost { get; set; }
    }
}
