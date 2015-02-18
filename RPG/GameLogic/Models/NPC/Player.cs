using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.Helpers;

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

    class Player : Npc, IPlayer
    {
        private KeyboardState ks;
        private Vector2 position;

        public Player(string id, string name, int health,
            int attackPower, int defense, int inventorySize,
            List<Stat> otherStats)
            : base(id, name, health, defense)
        {
            this.AttackPower = new Attack(attackPower);
            this.Inventory = new Inventory(inventorySize);
            this.OtherStats = otherStats;
            this.LeftAnimation = new Animation("Sprites\\Player\\character", 80f, 3, 1, true, this.Position.X, this.Position.Y);
            this.RightAnimation = new Animation("Sprites\\Player\\character", 80f, 3, 3, true, this.Position.X, this.Position.Y);
            this.BackAnimation = new Animation("Sprites\\Player\\character", 80f, 3, 2, true, this.Position.X, this.Position.Y);
            this.FrontAnimation = new Animation("Sprites\\Player\\character", 80f, 3, 4,true, this.Position.X, this.Position.Y);
            this.CurrentAnimation = LeftAnimation;
        }

        public Player(string id, string name, int health,
             int attackPower, int defense, int inventorySize)
            : this(id, name, health, attackPower, defense, inventorySize, null)
        {
        }

        public Texture2D PlayerTexture { get; set; }
        
        public Animation LeftAnimation { get; set; }
        
        public Animation RightAnimation { get; set; }
        
        public Animation BackAnimation { get; set; }
        
        public Animation FrontAnimation { get; set; }

        public Animation CurrentAnimation { get; set; }  

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
            throw new System.NotImplementedException();
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


        public void Update(GameTime gameTime)
        {
            this.KeyListener();
            this.CurrentAnimation.PlayAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.CurrentAnimation.Draw(spriteBatch, this.Position);
        }

        public void KeyListener()
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Right))
            {
                this.Position.X += 2f;
                this.CurrentAnimation = this.RightAnimation;
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                this.Position.X -= 2f;
                this.CurrentAnimation = this.LeftAnimation;
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                this.Position.Y -= 2f;
                this.CurrentAnimation = this.FrontAnimation;
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                this.Position.Y += 2f;
                this.CurrentAnimation = this.BackAnimation;
            }
        }
    }
}