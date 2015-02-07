using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.NPC.Base
{
    class Player: IPlayer, IMove, IFight
    {
        public Inventory.Base.Inventory Inventory { get; private set; }
        public List<Stat> Stats { get; private set; }
        public Attack AttackPower { get; private set; }
        public void Attack(Npc target)
        {
            throw new NotImplementedException();
        }
    }
}
