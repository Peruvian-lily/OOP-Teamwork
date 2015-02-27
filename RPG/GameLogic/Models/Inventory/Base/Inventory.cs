namespace RPG.GameLogic.Models.Inventory.Base
{
    using System;
    using System.Collections.Generic;
    using RPG.GameLogic.Models.PickUps;

    public class Inventory
    {
        private int _capacity;

        public Inventory(int capacity)
        {
            this.Capacity = capacity;
            this.Content = new List<Item>(capacity);
        }

        public List<Item> Content
        {
            get; 
            private set;
        }

        public int Capacity
        {
            get
            {
                return this._capacity;
            }

            private set
            {
                if (value < 5)
                {
                    throw new ArgumentOutOfRangeException("Inventory size must be higher than 5");
                }
                this._capacity = value;
            }
        }

        public void Add(Item item)
        {
            if (this.Content.Count < this._capacity)
            {
                this.Content.Add(item);
            }
        }

        public void Remove(Item item)
        {
            if (this.Content.Contains(item))
            {
                this.Content.Remove(item);
            }
        }

        public void Expand(int size)
        {
            this.Capacity += size;
            this.Content.AddRange(new List<Item>(size));
        }
    }
}
