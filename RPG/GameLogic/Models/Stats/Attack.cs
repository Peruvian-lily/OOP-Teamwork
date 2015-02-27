namespace RPG.GameLogic.Models.Stats
{
    using RPG.GameLogic.Models.Stats.Base;

    public class Attack : Stat
    {
        public Attack(int value) 
            : base("Attack", value, StatType.Offensive)
        {
        }
    }
}
