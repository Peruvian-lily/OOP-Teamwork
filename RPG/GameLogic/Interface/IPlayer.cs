
using System.Collections.Generic;
using RPG.GameLogic.Models.Inventory.Base;
using RPG.GameLogic.Models.PickUps.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Interface
{
    public interface IPlayer : IMove, IFight
    {
        Inventory Inventory { get; }
        void PickUp(PickUp item);
    }
}