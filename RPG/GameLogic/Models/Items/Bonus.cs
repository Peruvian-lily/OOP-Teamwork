using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Items.Base;

namespace RPG.GameLogic.Models.Items
{
    class Bonus : PickUp
    {

        public Bonus(string id, string name) : base(id, name)
        {
        }

        public void Apply(Character target)
        {
            
        }
    }
}
