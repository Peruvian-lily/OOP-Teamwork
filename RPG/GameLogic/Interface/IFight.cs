using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Stats;

namespace RPG.GameLogic.Interface
{
    public interface IFight
    {
        Health Health { get; }
        Attack AttackPower { get; }
        Defense Defense { get; }
        void Attack(Character target);
        Character GetTarget();
    }
}