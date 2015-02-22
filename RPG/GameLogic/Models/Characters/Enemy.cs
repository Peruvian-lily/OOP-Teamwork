using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;
using RPG.Graphics;

namespace RPG.GameLogic.Models.Characters
{
    public class Enemy : Character, IRoam, IEnemy
    {
        public Enemy(string name, int health,
            int attack, int defense, List<Stat> otherStats)
            : base(name, health, defense)
        {
            this.AttackPower = new Attack(attack);
            this.Stats = otherStats;
            this.Animation = new Animation("Sprites\\Monster\\enemy_new", 80f, 2, 4, false, 0, 0);
            this.CollisionRect = new Rectangle((int)Position.X, (int)Position.Y + Animation.frameWidth,
                Animation.frameWidth, Animation.frameHeight);
        }

        public Enemy(string name, int health,
            int attack, int defense)
            : this(name, health, attack, defense, new List<Stat>())
        {
        }

        public Rectangle CollisionRect { get; set; }

        public Animation Animation { get; private set; }

        public Texture2D EnemyTexture { get; set; }

        #region Stats and Damage

        public Attack AttackPower { get; private set; }

        public List<Stat> Stats { get; private set; }

        public List<Stat> OffensiveStats
        {
            get { return this.Stats.Where(stat => stat.Type == StatType.Offensive).ToList(); }
        }

        public List<Stat> DefensiveStats
        {
            get { return this.Stats.Where(stat => stat.Type == StatType.Defensive).ToList(); }
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

        public Character GetTarget()
        {
            throw new System.NotImplementedException();
        }

        public List<IFight> GetAllies()
        {
            //Return all allies the unit coldies with.
            throw new System.NotImplementedException();
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
            CollisionRect = new Rectangle((int)Position.X, (int)Position.Y + Animation.frameWidth,
                Animation.frameWidth, Animation.frameHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Animation.Draw(spriteBatch, this.Position);
            this.Animation.Draw(spriteBatch, this.Position);
        }
    }
}