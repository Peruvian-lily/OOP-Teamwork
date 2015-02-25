
using System.Collections.Generic;
using RPG.GameLogic.Models.Stats.Base;
using RPG.GameLogic.Models.Characters.Base;

namespace RPG.GameLogic.Interface
{
    public interface IEnemy : IFight
    {
        List<Stat> Stats { get; }
        List<IFight> GetAllies(List<Character> allGameCharacters);
    }
}