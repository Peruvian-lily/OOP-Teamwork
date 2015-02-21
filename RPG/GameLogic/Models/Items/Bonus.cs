﻿using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Items.Base;

namespace RPG.GameLogic.Models.Items
{
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
