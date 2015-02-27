namespace RPG.GameLogic.Models.PickUps.Base
{
    using System;

    public abstract class PickUp : GameObject
    {
        private string name;

        protected PickUp(string name) 
        {
            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be empty");
                }
                this.name = value;
            }
        }

    }
}