using System;
using System.Collections.Generic;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Items.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Items
{
    public class Item : PickUp, IItem
    {
        private List<Stat> _stats;

        public Item(string id, string name, List<Stat> stats, List<Effects.Base.Effects> effects)
            : base(id, name)
        {
            this.Stats = stats;
            this.Effects = effects;
        }

        public Item(string id, string name, List<Stat> stats)
            : this(id, name,stats,null)
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

        public List<Effects.Base.Effects> Effects { get; private set; }
    }
}
