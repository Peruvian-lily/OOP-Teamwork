using System;
using RPG.GameLogic.Models.Stats;

namespace RPG.GameLogic.Models.Characters.Base
{
    public abstract class Character : GameObject
    {
        private string name;

        protected Character(string id, string name, int health, int defense)
            : base(id)
        {
            this.Name = name;
            this.Health = new Health(health);
            this.Defense = new Defense(defense);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (value == null || value.Length < 2)
                {
                    throw new ArgumentOutOfRangeException("Name must be 2 or more symbols long.");
                }
                this.name = value;
            }
        }

        public Health Health { get; private set; }

        public Defense Defense { get; private set; }

        public void TakeDamage(int amount)
        {
            int damage = amount - this.Defense.Value;
            if (damage > 0)
            {
                this.Health.Reduce(damage);
            }
        }
    }
}
