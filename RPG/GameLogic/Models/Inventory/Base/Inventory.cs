using System.Collections.Generic;
using RPG.GameLogic.Models.Items;

namespace RPG.GameLogic.Models.Inventory.Base
{
    public class Inventory
    {
        private List<Item> _content;
        private int _size;

        public Inventory(int size)
        {
            this.Size = size;
            this.Content = new List<Item>();
        }

        public List<Item> Content { get; private set; }
        public int Size { get; private set; }
        public void Add(Item item)
        {
            this.Content.Add(item);
        }
        public void Remove(Item item)
        {
            this.Content.Remove(item);
        }
    }
}
