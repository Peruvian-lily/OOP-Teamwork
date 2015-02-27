namespace RPG.GameLogic.Interface
{
    using System.Collections.Generic;
    using RPG.GameLogic.Models.Effects.Base;
    using RPG.GameLogic.Models.Inventory.Base;
    using RPG.GameLogic.Models.PickUps.Base;
    using RPG.GameLogic.Models.Spells.Base;

    public interface IPlayer : IMove, IFight
    {
        Inventory Inventory { get; }

        List<Skill> Skills { get; }

        List<Effects> Effects { get; }

        void PickUp(PickUp item);
    }
}