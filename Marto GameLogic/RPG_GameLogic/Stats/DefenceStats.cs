using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_GameLogic.Stats
{
    public class DefenceStats : Stat
    {
        private int physicalArmor;
        private int fireResist;
        private int iceResist;
        private int lightningResist;
        private int poisonResist;

        public DefenceStats(int physicalArmor, int fireResist, int iceResist, int lightningResist, int poisonResist)
        {
            this.PhysicalArmor = physicalArmor;
            this.FireResist = fireResist;
            this.IceResist = iceResist;
            this.LightningResist = lightningResist;
            this.PoisonResist = poisonResist;
        }
        public DefenceStats()
            : this(0, 0, 0, 0, 0) 
        { 
        }

        public int PhysicalArmor 
        {
            get { return this.physicalArmor; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Physical armor stat can't be negative.");
                }

                this.physicalArmor = value;
            }
        }
        public int FireResist
        {
            get { return this.fireResist; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fire resist stat can't be negative.");
                }

                this.fireResist = value;
            }
        }

        public int IceResist
        {
            get { return this.iceResist; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ice resist stat can't be negative.");
                }

                this.iceResist = value;
            }
        }
        public int LightningResist
        {
            get { return this.lightningResist; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Lightning resist stat can't be negative.");
                }

                this.lightningResist = value;
            }
        }
        public int PoisonResist
        {
            get { return this.poisonResist; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Poison resist stat can't be negative.");
                }

                this.poisonResist = value;
            }
        }
    }
}
