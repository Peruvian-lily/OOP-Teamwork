namespace RPG.GameLogic.Models.Stats
{
    using RPG.GameLogic.Models.Stats.Base;

    public class Fire : Stat
    {
        public Fire(int value) 
            : base("Fire", value, StatType.Offensive)
        {
        }
    }
}
