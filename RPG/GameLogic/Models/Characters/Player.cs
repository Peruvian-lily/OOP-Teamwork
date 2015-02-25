using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.PickUps;
using RPG.GameLogic.Models.PickUps.Base;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;
using RPG.Graphics;
using RPG.Graphics.Map;

namespace RPG.GameLogic.Models.Characters
{
    public class Player : Character, IPlayer
    {
        public const int speed = 2;
        private const int remainderScreenSize = 1;
        
        public readonly int cameraMaxWidth = (TileMap.mapWidth - remainderScreenSize) * Tile.TileWidth - Game1.bufferWidth;
        public readonly int cameraMaxHeight = (TileMap.mapHeight - remainderScreenSize)*Tile.TileHeight -
                                              Game1.bufferHeight;
        public static readonly Vector2 UP_VECTOR = new Vector2(0, -1);
        public static readonly Vector2 DOWN_VECTOR = new Vector2(0, 1);
        public static readonly Vector2 RIGHT_VECTOR = new Vector2(1, 0);
        public static readonly Vector2 LEFT_VECTOR = new Vector2(-1, 0);

        private KeyboardState ks;
        private byte lastFrame = 3;
        private List<Stat> otherStats;

        public Player(string name, int health,
            int attackPower, int defense, int inventorySize,
            List<Stat> otherStats)
            : base(name, health, defense)
        {
            this.AttackPower = new Attack(attackPower);
            this.Inventory = new Inventory.Base.Inventory(inventorySize);
            this.otherStats = otherStats;
            this.AddAnimation();
        }

        public Player(string name, int health,
             int attackPower, int defense, int inventorySize)
            : this(name, health, attackPower, defense, inventorySize, new List<Stat>())
        {
        }

        public Rectangle CollisionRect
        {
            get 
            { 
                return new Rectangle((int)Position.X, (int)Position.Y, this.Height, this.Width); 
            }
        }

        #region Animation
        public Animation LeftAnimation { get; set; }

        public Animation RightAnimation { get; set; }

        public Animation BackAnimation { get; set; }

        public Animation FrontAnimation { get; set; }

        public Animation CurrentAnimation { get; set; }
        #endregion

        #region Stats and Damage
        public Attack AttackPower { get; private set; }
        public List<Effect> Spells { get; private set; }
        public List<Stat> OffensiveStats
        {
            get { return this.otherStats.Where(stat => stat.Type == StatType.Offensive).ToList(); }
        }

        public List<Stat> DefensiveStats
        {
            get { return this.otherStats.Where(stat => stat.Type == StatType.Defensive).ToList(); }
        }

        public void Attack(IFight target)
        {
            int damage = this.AttackPower.Value;
            this.OffensiveStats.ForEach(stat =>
            {
                damage += stat.Value;
            });
            target.TakeDamage(damage, this.OffensiveStats);
        }

        public override void TakeDamage(int amount, List<Stat> types)
        {
            int reduction = this.Defense.Value;
            types.ForEach(type =>
            {
                if (this.DefensiveStats.Contains(type))
                {
                    reduction += this.DefensiveStats.Find(stat => stat.Equals(type)).Value;
                }
            });
            if (amount > reduction)
            {
                this.Health.Reduce(amount - reduction);
            }
        }
        #endregion

        #region Inventory
        public Inventory.Base.Inventory Inventory { get; private set; }

        public void PickUp(PickUp item)
        {
            string itemType = item.GetType().Name;

            switch (itemType)
            {
                case "Item":
                    this.Inventory.Add((Item)item);
                    this.ApplyItemStats((Item)item);
                    break;
                case "bonus":
                    ((Bonus)item).Apply(this);
                    break;
            }
        }

        private void ApplyItemStats(Item item)
        {
            foreach (var stat in item.Stats)
            {
                switch (stat.Name)
                {
                    case "Attack":
                        this.AttackPower.Increase(stat.Value);
                        break;
                    case "Health":
                        this.Health.Increase(stat.Value);
                        break;
                    case "Defense":
                        this.Defense.Increase(stat.Value);
                        break;
                    default:
                        if (this.otherStats.Contains(stat))
                        {
                            this.otherStats.Find(entry => entry == stat).Increase(stat.Value);
                        }
                        else
                        {
                            this.otherStats.Add(stat);
                        }
                        break;
                }
            }
        }
        #endregion

        public Character GetTarget()
        {
            throw new System.NotImplementedException();
        }

        #region Engine Stuff
        public override void Update(GameTime gameTime)
        {
            this.KeyListener();
            this.CurrentAnimation.PlayAnimation(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
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
            if (Camera.Location.X == 0 || Camera.Location.X == cameraMaxWidth)
            {
                this.Position.X += speed * direction.X;
            }
            if (Camera.Location.Y == 0 || Camera.Location.Y == cameraMaxHeight)
            {
                this.Position.Y += speed * direction.Y;   
            }
        }

        private void MoveLeft()
        {
            if (0 >= this.Position.X + speed)
            {
                return;
            }

            this.lastFrame = 1;
            this.CurrentAnimation = this.LeftAnimation;
            Move(LEFT_VECTOR);
        }

        private void MoveUp()
        {
            if (0 >= this.Position.Y + speed)
            {
                return;
            }

            this.lastFrame = 4;
            this.CurrentAnimation = this.FrontAnimation;
            Move(UP_VECTOR);
        }

        private void MoveDown()
        {
            if (Game1.ScreenHeight <= this.Position.Y + speed + this.Height)
            {
                return;
            }

            this.lastFrame = 2;
            this.CurrentAnimation = this.BackAnimation;
            Move(DOWN_VECTOR);
        }

        private void MoveRight()
        {
            if (Game1.ScreenWidth <= this.Position.X + speed + this.Width)
            {
                return;
            }

            this.lastFrame = 3;
            this.CurrentAnimation = this.RightAnimation;
            Move(RIGHT_VECTOR);
        }
        #endregion

        private void AddAnimation()
        {
            this.LeftAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                1, true, this.Position.X, this.Position.Y);
            this.RightAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                3, true, this.Position.X, this.Position.Y);
            this.BackAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                2, true, this.Position.X, this.Position.Y);
            this.FrontAnimation = new Animation("Sprites\\Player\\character", 80f, 3,
                4, true, this.Position.X, this.Position.Y);
            this.Texture = Game1.Content.Load<Texture2D>("Sprites\\Player\\character");
            this.CurrentAnimation = LeftAnimation;
        }
        #endregion
    }
}