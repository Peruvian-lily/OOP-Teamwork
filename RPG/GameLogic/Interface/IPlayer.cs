
using System.Collections.Generic;
using RPG.GameLogic.Models.Inventory.Base;
using RPG.GameLogic.Models.Items.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Interface
{
    public interface IPlayer : IMove, IFight
    {
        Inventory Inventory { get; }
        List<Stat> OtherStats { get; }

        void PickUp(PickUp item);
    }
}