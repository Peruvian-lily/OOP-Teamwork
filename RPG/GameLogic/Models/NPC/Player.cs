using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.GameLogic.Models.NPC
{
    using System.Collections.Generic;
    using Interface;
    using Base;
    using Inventory;
    using Inventory.Base;
    using Items;
    using Items.Base;
    using Stats;
    using Stats.Base;

    class Player : Npc, IPlayer, IDraw
    {
        private Texture2D player;

        public Player(string id, string name, int health,
            int attackPower, int defense, int inventorySize,
            List<Stat> otherStats)
            : base(id, name, health, defense)
        {
            this.AttackPower = new Attack(attackPower);
            this.Inventory = new Inventory(inventorySize);
            this.OtherStats = otherStats;
        }

        public Player(string id, string name, int health,
             int attackPower, int defense, int inventorySize)
            : this(id, name, health, attackPower, defense, inventorySize, null)
        {
        }

        public Attack AttackPower { get; private set; }

        public Inventory Inventory { get; private set; }

        public List<Stat> OtherStats { get; private set; }

        public void Attack(Npc target)
        {
            int damage = this.AttackPower.Value; //Add more values to formula as more stat types get implemented.
            target.TakeDamage(damage);
        }

        public Npc GetTarget()
        {
            return null;
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void PickUp(PickUp item)
        {
            string itemType = item.GetType().Name.ToLower();

            switch (itemType)
            {
                case "item":
                    this.Inventory.Add((Item)item);
                    break;
                case "bonus":
                    ((Bonus)item).Apply(this);
                    break;
            }
        }

        public void LoadContent()
        {
            
        }

        public void Updete()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}