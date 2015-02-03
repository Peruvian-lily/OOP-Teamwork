using System;

namespace RPG_GameLogic.Stats
{
    public class AttackStats : Stat
    {
        private int physical;
        private int fire;
        private int ice;
        private int lightning;
        private int poison;

        public AttackStats(int physical, int fire, int ice, int lightning, int poison)
        {
            this.Physical = physical;
            this.Fire = fire;
            this.Ice = ice;
            this.Lightning = lightning;
            this.Poison = poison;
        }
        public AttackStats()
            : this(0, 0, 0, 0, 0)
        {
        }

        public int Physical 
        {
            get { return this.physical; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Physical attack stat can't be negative.");
                }

                this.physical = value;
            }
        }
        public int Fire 
        {
            get { return this.fire; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fire attack stat can't be negative.");
                }

                this.fire = value;
            }
        }

        public int Ice
        {
            get { return this.ice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ice attack stat can't be negative.");
                }

                this.ice = value;
            }
        }
        public int Lightning
        {
            get { return this.lightning; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Lightning attack stat can't be negative.");
                }

                this.lightning = value;
            }
        }
        public int Poison
        {
            get { return this.poison; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Poison attack stat can't be negative.");
                }

                this.poison = value;
            }
        }
    }
}
