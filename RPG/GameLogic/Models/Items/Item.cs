namespace RPG.GameLogic.Models.Items
{
    using System;
    using System.Collections.Generic;
    using Interface;
    using Base;
    using Stats.Base;
    using Effects.Base;

    public class Item : PickUp, IItem
    {
        private List<Stat> _stats;

        public Item(string id, string name, List<Stat> stats, List<Effects> effects)
            : base(id, name)
        {
            this.Stats = stats;
            this.Effects = effects;
        }

        public Item(string id, string name, List<Stat> stats)
            : this(id, name, stats, null)
        {

        }

        public List<Stat> Stats
        {
            get { return this._stats; }
            private set
            {
                if (value.Count < 2)
                {
                    throw new ArgumentOutOfRangeException("Item requires atleast 2 stats");
                }
                this._stats = value;
            }
        }

        public List<Effects> Effects { get; private set; }
    }
}
