﻿namespace RPG.GameLogic.Models.PickUps
{
    using System;
    using System.Collections.Generic;
    using RPG.GameLogic.Interface;
    using RPG.GameLogic.Models.PickUps.Base;
    using RPG.GameLogic.Models.Stats.Base;

    public class Item : PickUp, IItem
    {
        private List<Stat> stats;

        public Item(string name, List<Stat> stats, Effects.Base.Effects effect)
            : base(name)
        {
            this.Stats = stats;
            this.Effect = effect;
        }

        public Item(string id, string name, List<Stat> stats)
            : this(name, stats, null)
        {
        }

        public List<Stat> Stats
        {
            get
            {
                return this.stats;
            }

            private set
            {
                if (value.Count < 2)
                {
                    throw new ArgumentOutOfRangeException("Item requires atleast 2 stats");
                }

                this.stats = value;
            }
        }

        public Effects.Base.Effects Effect { get; private set; }
    }
}
