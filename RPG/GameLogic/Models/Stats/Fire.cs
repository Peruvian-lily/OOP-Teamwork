using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Stats
{
    class Fire : Stat
    {
        public Fire(int value) 
            : base("Fire", value, StatType.Offensive)
        {
        }
    }
}
