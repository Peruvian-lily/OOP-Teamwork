
namespace RPG.GameLogic.Models.Stats
{
    using Base;
    class FireAttack : Stat
    {
        public FireAttack(int value) 
            : base("Fire Damage", value, StatType.Offensive)
        {

        }
    }
}
