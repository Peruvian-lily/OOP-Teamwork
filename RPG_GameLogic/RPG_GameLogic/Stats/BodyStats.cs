using System;

namespace RPG_GameLogic.Stats
{
    public class BodyStats : Stat
    {
        private int hp;
        private int mp;

        public BodyStats(int hp, int mp)
        {
            this.HP = hp;
            this.MP = mp;
        }
        public BodyStats()
            : this(0, 0)
        { 
        }

        public int HP 
        {
            get { return this.hp; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("HP stat can't be negative");
                }

                this.hp = value;
            }
        }

        public int MP
        {
            get { return this.mp; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("MP stat can't be negative");
                }

                this.mp = value;
            }
        }
    }
}
