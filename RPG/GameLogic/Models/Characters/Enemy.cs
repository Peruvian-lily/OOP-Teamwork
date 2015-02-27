namespace RPG.GameLogic.Models.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using RPG.GameLogic.Core.Factory;
    using RPG.GameLogic.Interface;
    using RPG.GameLogic.Models.Characters.Base;
    using RPG.GameLogic.Models.Stats;
    using RPG.GameLogic.Models.Stats.Base;
    using RPG.Graphics;
    using RPG.Graphics.CustomShapes;

    public class Enemy : Character, IRoam, IEnemy
    {
        private const int CollisonCirlceRadius = 10;

        public Enemy(string name, int health, int attack, int defense, List<Stat> otherStats, EnemyType enemyType)
            : base(name, health, defense)
        {
            this.AttackPower = new Attack(attack);
            this.Stats = otherStats;
            this.EnemyType = enemyType;
            this.Animation = new Animation("Sprites\\Monster\\enemy1", 80f, 3, 2, false, 0, 0);
            this.CollisionRect = new Rectangle((int)Position.X, (int)Position.Y + Animation.FrameWidth, Animation.FrameWidth, Animation.FrameHeight);
        }

        public Enemy(string name, int health, int attack, int defense, EnemyType enemyType)
            : this(name, health, attack, defense, new List<Stat>(), enemyType)
        {
        }

        public EnemyType EnemyType { get; set; }

        public Rectangle CollisionRect { get; set; }

        public Animation Animation { get; private set; }

        public Texture2D EnemyTexture { get; set; }

        #region Stats and Damage

        public Attack AttackPower { get; private set; }

        public List<Stat> Stats { get; private set; }

        public List<Stat> OffensiveStats
        {
            get
            {
                return this.Stats.Where(stat => stat.Type == StatType.Offensive).ToList();
            }
        }

        public List<Stat> DefensiveStats
        {
            get
            {
                return this.Stats.Where(stat => stat.Type == StatType.Defensive).ToList();
            }
        }

        public Circle CollisonCircle
        {
            get
            {
                return new Circle(this.Position, CollisonCirlceRadius);
            }
        }

        public void Attack(IFight target)
        {
            int damage = this.AttackPower.Value;
            this.OffensiveStats.ForEach(stat =>
            {
                damage = +stat.Value;
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

        public List<Character> GetAllies(List<Character> allGameCharacters)
        {
            var allaysThatCanHelp = new List<Character>();

            foreach (var character in allGameCharacters)
            {
                if (character is IFight && character is Enemy)
                {
                    Enemy currEnemy = character as Enemy;
                    Vector2 enemyCenter = new Vector2(currEnemy.CollisionRect.Center.X, currEnemy.CollisionRect.Center.Y);
                    if (this.CollisonCircle.IsPointInCirlce(enemyCenter))
                    {
                        allaysThatCanHelp.Add(currEnemy);
                    }
                }
            }

            return allaysThatCanHelp;
        }

        public void Roam()
        {
            throw new System.NotImplementedException();
        }
        public void Initialize(Texture2D texture, Vector2 position)
        {
            this.EnemyTexture = texture;
            this.Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            this.Animation.PlayAnimation(gameTime);
            this.Animation.PlayAnimation(gameTime);
            this.CollisionRect = new Rectangle((int)Position.X, (int)Position.Y + Animation.FrameWidth, Animation.FrameWidth, Animation.FrameHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Animation.Draw(spriteBatch, this.Position);
            this.Animation.Draw(spriteBatch, this.Position);
        }
    }
}