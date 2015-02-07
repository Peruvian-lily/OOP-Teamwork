

using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.NPC.Base;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Models.Effects.Base
{
    public abstract class Effects : ITimeout
    {

        protected Effects(Stat stat, Npc target, int duration)
        {
            this.Stat = stat;
            this.Target = target;
            this.Duration = duration;
        }
        protected Effects(Stat stat, Npc target)
            : this(stat, target, 1)
        {
            
        }

        public Stat Stat { get; private set; }
        public Npc Target { get; private set; }
        public int Duration { get; private set; }

        public abstract void Apply();
    }
}
