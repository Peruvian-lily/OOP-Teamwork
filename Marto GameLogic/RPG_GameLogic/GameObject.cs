using System;
using RPG_GameLogic.Interfaces;

namespace RPG_GameLogic
{
    public abstract class GameObject : IGameObject
    {
        private int id;

        public GameObject(int id) 
        {
            this.ID = id;
        }
        public GameObject() : this(0) 
        {
        }

        public int ID 
        {
            get { return this.id; }
            set 
            {
                if (id < 0)
                {
                    throw new ArgumentNullException("Id can't be null");
                }

                this.id = value;
            }
        }
    }
}
