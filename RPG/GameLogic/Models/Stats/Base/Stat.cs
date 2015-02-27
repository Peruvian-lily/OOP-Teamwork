﻿using RPG.GameLogic.Exceptions;

namespace RPG.GameLogic.Models.Stats.Base
{
    using System;

    public abstract class Stat
    {
        private string name;
        private int value;

        protected Stat(string name, int value, StatType type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Stats name cannot be empty");
                }

                this.name = value;
            }
        }
        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeStatsException();
                }

                this.value = value;
            }
        }

        public StatType Type { get; set; }

        public void Reduce(int amount)
        {
            if (this.Value - amount < 0)
            {
                this.Value = 0;
            }
            else
            {
                this.Value -= amount;
            }
        }
        public void Increase(int amount)
        {
            this.Value += amount;
        }

        public override bool Equals(object obj)
        {
            var stat = obj as Stat;
            if (stat != null)
            {
                return this.Name == stat.Name;
            }

            return false;
        }
    }
}
