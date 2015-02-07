using System;

namespace RPG.GameLogic.Models.Items.Base
{
    public abstract class PickUp : GameObject
    {
        private string _name;

        protected PickUp(string id, string name) 
            : base(id)
        {
            this.Name = name;
        }

        public string Name
        {
            get { return this._name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be empty");
                }
                this._name = value;
            }
        }

    }
}