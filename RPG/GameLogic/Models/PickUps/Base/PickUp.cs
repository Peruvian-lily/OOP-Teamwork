using System;

namespace RPG.GameLogic.Models.PickUps.Base
{
    public abstract class PickUp : GameObject
    {
        private string _name;

        protected PickUp(string name) 
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