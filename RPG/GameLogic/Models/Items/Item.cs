﻿namespace RPG.GameLogic.Models.Items
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

        public Item(string id, string name, List<Stat> stats, Effects effect)
            : base(id, name)
        {
            this.Stats = stats;
            this.Effect = effect;
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

        public Effects Effect { get; private set; }
    }
}
