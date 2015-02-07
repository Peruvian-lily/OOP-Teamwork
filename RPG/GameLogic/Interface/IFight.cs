

using RPG.GameLogic.Models.NPC.Base;
using RPG.GameLogic.Models.Stats;

namespace RPG.GameLogic.Interface
{
    public interface IFight
    {
        Attack AttackPower { get; }
        void Attack(Npc target);
    }
}