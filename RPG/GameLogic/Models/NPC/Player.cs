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
        private const float Speed = 2;
        private KeyboardState ks;
        private byte lastFrame = 3;

        public Player(string id, string name, int health,
            int attackPower, int defense, int inventorySize,
            List<Stat> otherStats)
            : base(id, name, health, defense)
        {
            this.AttackPower = new Attack(attackPower);
            this.Inventory = new Inventory(inventorySize);
            this.OtherStats = otherStats;
            this.LeftAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                1, true, this.Position.X, this.Position.Y);
            this.RightAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                3, true, this.Position.X, this.Position.Y);
            this.BackAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                2, true, this.Position.X, this.Position.Y);
            this.FrontAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                4, true, this.Position.X, this.Position.Y);
            this.PlayerTexture = Game1.Content.Load<Texture2D>("Sprites\\Player\\character");
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
            get 
            {
                // In the sprite, we are using, the character is drawn 3 times 
                // on the X axis so to get his width he divide by 3!
                const int SPRITE_WIDTH_OFFSET = 3;
                return this.PlayerTexture.Width / SPRITE_WIDTH_OFFSET; 
            }
        }

        public int Height
        {
            get 
            {
                // In the sprite, we are using, the character is drawn 4 times 
                // on the Y axis so to get his height he divide by 4!
                const int SPRITE_HEIGHT_OFFSET = 4;
                return this.PlayerTexture.Height / SPRITE_HEIGHT_OFFSET; 
            }
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
            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Left) ||
                ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.Down))
            {
                if (ks.IsKeyDown(Keys.Right))
                {
                    this.MoveRight();
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    this.MoveLeft(); ;
                }
                if (ks.IsKeyDown(Keys.Up))
                {
                    this.MoveUp();
                }
                else if (ks.IsKeyDown(Keys.Down))
                {
                    this.MoveDown();
                }
            }
            else
            {
                this.CurrentAnimation = new Animation("Sprites\\Player\\character", 80f, 3, lastFrame, 
                    false, this.Position.X, this.Position.Y);
            }
        }

        #region Movement
        public void Move(Vector2 direction)
        {
            this.Position.X += Speed * direction.X;
            this.Position.Y += Speed * direction.Y;
        }

        private void MoveLeft()
        {
            if (0 >= this.Position.X + Speed)
            {
                return;
            }

            this.lastFrame = 1;
            this.CurrentAnimation = this.LeftAnimation;
            Move(Game1.LEFT_VECTOR);
        }

        private void MoveUp()
        {
            if (0 >= this.Position.Y + Speed)
            {
                return;
            }

            this.lastFrame = 4;
            this.CurrentAnimation = this.FrontAnimation;
            Move(Game1.UP_VECTOR);
        }

        private void MoveDown()
        {
            if (Game1.ScreenHeight <= this.Position.Y + Speed + this.Height)
            {
                return;
            }

            this.lastFrame = 2;
            this.CurrentAnimation = this.BackAnimation;
            Move(Game1.DOWN_VECTOR);
        }

        private void MoveRight()
        {
            if (Game1.ScreenWidth <= this.Position.X + Speed + this.Width)
            {
                return;
            }

            this.lastFrame = 3;
            this.CurrentAnimation = this.RightAnimation;
            Move(Game1.RIGHT_VECTOR);
        }
        #endregion
    }
}