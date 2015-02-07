﻿using System;

namespace RPG.GameLogic.Models.Stats.Base
{
    public abstract class Stat
    {
        private string _name;
        private int _value;

        protected Stat(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name
        {
            get { return this._name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Stats name cannot be empty");
                }
            }
        }

        public int Value
        {
            get { return this._value; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Stats value cannot be negative");
                }
                this._value = value;
            }
        }
    }
}
