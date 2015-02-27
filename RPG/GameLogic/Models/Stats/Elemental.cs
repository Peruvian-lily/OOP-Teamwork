namespace RPG.GameLogic.Models.Stats
{
    using Base;

    class Elemental : Stat
    {
        public Elemental(int value, StatType type) 
            : base("Elemental", value, type)
        {
        }
    }
}
