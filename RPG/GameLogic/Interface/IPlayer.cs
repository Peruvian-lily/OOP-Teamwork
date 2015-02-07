
using System.Collections.Generic;
using RPG.GameLogic.Models.Inventory.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Interface
{
    public interface IPlayer
    {
        Inventory Inventory { get; }
        List<Stat> Stats { get; } 
    }
}