using System;
using RPG_GameLogic.Interfaces;

namespace RPG_GameLogic.Stats
{
    public class StatsList
    {
        private AttackStats attackStats;
        private DefenceStats defenceStats;
        private BodyStats bodyStats;
        private CritStats critStats;

        public StatsList(AttackStats attackStats, DefenceStats defenceStats, BodyStats bodyStats, CritStats critStats) 
        {
            this.АttackStats = attackStats;
            this.DefenceStats = defenceStats;
            this.BodyStats = bodyStats;
            this.CritStats = critStats;
        }
        public StatsList()
            : this(new AttackStats(), new DefenceStats(), new BodyStats(), new CritStats())
        { 
        }

        public AttackStats АttackStats 
        {
            get { return this.attackStats; }
            set
            {
                this.attackStats = value;
            }
        }

        public DefenceStats DefenceStats
        {
            get { return this.defenceStats; }
            set
            {
                this.defenceStats = value;
            }
        }

        public BodyStats BodyStats
        {
            get { return this.bodyStats; }
            set
            {
                this.bodyStats = value;
            }
        }

        public CritStats CritStats
        {
            get { return this.critStats; }
            set
            {
                this.critStats = value;
            }
        }

        public override string ToString()
        {
            string attackStatsStr = String.Format("Attack : Physical attack = {0}, Fire attack = {1},Ice attack = {2},\n\t\tLightning attack = {3}, Poison attack = {4}\n",
                this.АttackStats.Physical, this.АttackStats.Fire, this.АttackStats.Ice, this.АttackStats.Lightning, this.АttackStats.Poison);

            string defenceStatsStr = String.Format("Defence : Physical armor = {0}, Fire resist = {1},Ice resist = {2},\n\t\tLightning resist = {3}, Poison resist = {4}\n",
                this.DefenceStats.PhysicalArmor, this.DefenceStats.FireResist, this.DefenceStats.IceResist, this.DefenceStats.LightningResist, this.DefenceStats.PoisonResist);

            string bodyStatStr = String.Format("Hit points = {0}, Mana points = {1}\n", this.BodyStats.HP, this.BodyStats.MP);

            string critStatsStr = String.Format("Critical chance = {0}, Critical damage = {1}\n", this.CritStats.CritChange, this.CritStats.CritDamage);

            return String.Format("Stats : {{\n\t{0}\n\t{1}\n\t{2}\n\t{3}}}", attackStatsStr, defenceStatsStr, bodyStatStr, critStatsStr);
        }
    }
}
