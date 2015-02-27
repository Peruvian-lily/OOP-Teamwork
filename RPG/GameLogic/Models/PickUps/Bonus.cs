namespace RPG.GameLogic.Models.PickUps
{
    using RPG.GameLogic.Models.Characters.Base;
    using RPG.GameLogic.Models.PickUps.Base;

    class Bonus : PickUp
    {

        public Bonus(string name) 
            : base(name)
        {
        }

        public void Apply(Character target)
        {
        }
    }
}
