using Microsoft.Xna.Framework;
using RPG.GameLogic.Interface;

namespace RPG.GameLogic.Models.NPC.Base
{
    using System;
    using Stats;
    using Stats.Base;

    public abstract class Npc : GameObject
    {
        private Vector2 position;
        private string _name;

        protected Npc(string id, string name, int health, int defense)
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
                return this._name;
            }
            private set
            {
                if (value == null || value.Length < 2)
                {
                    throw new ArgumentOutOfRangeException("Name must be 2 or more symbols long.");
                }
                this._name = value;
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
