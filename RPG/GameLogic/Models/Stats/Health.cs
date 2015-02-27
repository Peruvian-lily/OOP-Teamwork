namespace RPG.GameLogic.Models.Stats
{
    using RPG.GameLogic.Models.Stats.Base;

    public class Health : Stat
    {
        public Health(int value) 
            : base("Health", value, StatType.Defensive)
        {
        }
    }
}
