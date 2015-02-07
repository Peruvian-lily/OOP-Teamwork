using System;

namespace RPG_GameLogic.Stats
{
    public class CritStats : Stat
    {
        private int critChance;
        private int critDamage;

        public CritStats(int critChance, int critDmg) 
        {
            this.CritChange = critChance;
            this.CritDamage = critDmg;
        }
        public CritStats()
            : this(0, 0)
        { 
        }

        public int CritChange 
        {
            get { return this.critChance; }
            set 
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Critical chance stat is percentage based so it must be in the range [0..100].");
                }

                this.critChance = value;
            }
        }

        public int CritDamage
        {
            get { return this.critDamage; }
            set
            {
                if (value < 0 || value > 5)
                {
                    throw new ArgumentException("The critical damage stat is a damage multiplier so it must be very small [0..5]");
                }

                this.critDamage = value;
            }
        }
    }
}
