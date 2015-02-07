using System;
using RPG_GameLogic.Stats;

namespace RPG_GameLogic.Interfaces
{
    interface IStatistics
    {
        StatsList Stats { get; }
    }
}
