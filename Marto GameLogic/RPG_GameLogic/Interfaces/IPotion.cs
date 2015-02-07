using System;
using RPG_GameLogic.Stats;

namespace RPG_GameLogic.Interfaces
{
    public interface IPotion
    {
        StatsList Effect();
        bool isPermanent { get; set; }
    }
}
