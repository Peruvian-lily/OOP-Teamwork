
using System.Collections.Generic;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Interface
{
    public interface IEnemy
    {
        List<Stat> Stats { get; }
    }
}