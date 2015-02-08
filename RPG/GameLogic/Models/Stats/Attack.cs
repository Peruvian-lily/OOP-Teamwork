using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Stats
{
    public class Attack : Stat
    {
        public Attack(int value) 
            : base("Attack", value, StatType.Offensive)
        {
        }
    }
}
