using System;

namespace Game.Models.Npc.Base
{
    abstract class Npc : GameObject
    {
        private string _name;

        protected Npc(string id, string name)
            : base(id)
        {
            this.Name = name;
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
    }
}
