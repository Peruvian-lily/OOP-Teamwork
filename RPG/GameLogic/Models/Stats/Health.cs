
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Stats
{
    public class Health : Stat
    {
        public Health(int value) 
            : base("Health", value,StatType.Defensive)
        {
        }
    }
}
