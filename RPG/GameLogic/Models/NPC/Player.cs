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

    class Player : Npc, IPlayer, IDrawable
    {
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

        public Texture2D PlayerTexture { get; set; }

        public Vector2 PlayerPosition { get; set; }

        public bool Active { get; set; }

        public int Width
        {
            get { return this.PlayerTexture.Width; }
        }

        public int Height
        {
            get { return this.PlayerTexture.Height; }
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

        public void Initialize(Texture2D texture, Vector2 position)
        {
            PlayerTexture = texture;
            PlayerPosition = position;
            Active = true;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, PlayerPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        } 
    }
}