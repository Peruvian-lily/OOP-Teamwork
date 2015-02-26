using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Models.Effects.Base;
using RPG.GameLogic.Models.Inventory.Base;
using RPG.GameLogic.Models.PickUps.Base;
using RPG.GameLogic.Models.Spells.Base;

namespace RPG.GameLogic.Interface
{
    public interface IPlayer : IMove, IFight
    {
        Inventory Inventory { get; }
        void PickUp(PickUp item);
        List<Spell> Spells { get; }
        List<Effects> Effects { get; } 
    }
}