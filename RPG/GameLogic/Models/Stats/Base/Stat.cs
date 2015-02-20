using System;

namespace RPG.GameLogic.Models.Stats.Base
{
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
            get { return this.name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Stats name cannot be empty");
                }
                this.name = value;
            }
        }
        public int Value
        {
            get { return this.value; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Stats value cannot be negative or zero");
                }
                this.value = value;
            }
        }

        public StatType Type { get; set; }

        public void Reduce(int amount)
        {
            this.Value -= amount;
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
