using System.Collections.Generic;
using RPG.GameLogic.Models.Effects.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Interface
{
    public interface IItem
    {
        List<Stat> Stats { get; }
        Effects Effect { get; }
    }
}