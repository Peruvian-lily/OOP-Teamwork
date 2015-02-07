namespace RPG.GameLogic.Models.Inventory
{
    using Items.Base;
    using NPC.Base;

    class Bonus : PickUp
    {

        public Bonus(string id, string name) : base(id, name)
        {
        }

        public void Apply(Npc target)
        {
            
        }
    }
}
