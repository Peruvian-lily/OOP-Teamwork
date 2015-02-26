using System.Collections.Generic;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Interface
{
    public interface IFight
    {
        Health Health { get; }
        Attack AttackPower { get; }
        Defense Defense { get; }
        void Attack(IFight target);
        void TakeDamage(int amount, List<Stat> types);
    }
}