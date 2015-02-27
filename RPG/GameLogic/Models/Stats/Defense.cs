namespace RPG.GameLogic.Models.Stats
{
    using Base;

    public class Defense : Stat
    {
        public Defense(int value) 
            : base("Defense", value, StatType.Defensive)
        {
        }
    }
}
