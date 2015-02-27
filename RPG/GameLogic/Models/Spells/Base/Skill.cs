namespace RPG.GameLogic.Models.Spells.Base
{
    using System;
    using Effects.Base;
    using RPG.GameLogic.Models.Characters;
    using RPG.GameLogic.Models.Stats.Base;

    public abstract class Skill
    {
        protected Skill(string name, Stat stat, Player owner)
            : this(name, stat, null, owner)
        {
        }

        protected Skill(string name, Stat stat, Effects effect, Player owner)
        {
            this.Name = name;
            this.Stat = stat;
            this.Effect = effect;
            this.Owner = owner;
        }

        public Player Owner { get; set; }

        public string Name { get; set; }

        public Stat Stat { get; set; }

        public Effects Effect { get; set; }

        public int Cost { get; set; }
    }
}
