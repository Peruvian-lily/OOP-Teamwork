namespace RPG.GameLogic.Interface
{
    using System.Collections.Generic;
    using RPG.GameLogic.Models.Effects.Base;
    using RPG.GameLogic.Models.Stats.Base;

    public interface IItem
    {
        List<Stat> Stats { get; }

        Effects Effect { get; }
    }
}