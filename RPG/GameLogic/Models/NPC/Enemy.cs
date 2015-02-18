using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;
using RPG.Helpers;

namespace RPG.GameLogic.Models.NPC
{


    using Base;
    public class Enemy : Npc, IRoam, IEnemy
    {
        private Animation animation;

        public Enemy(string id, string name, int health,
            int attack, int defense, List<Stat> otherStats)
            : base(id, name, health, defense)
        {
            this.AttackPower = new Attack(attack);
            this.Stats = otherStats;
            this.animation = new Animation("Sprites\\Monster\\enemies", 80f, 3, false, 300, 100);
        }

        public Enemy(string id, string name, int health,
            int attack, int defense)
            : this(id, name, health, attack, defense, null)
        {
        }

        public Texture2D EnemyTexture { get; set; }

        public Vector2 EnemyPosition { get; set; }

        public List<Stat> Stats { get; private set; }

        public Attack AttackPower { get; private set; }

        public void Attack(Npc target)
        {
            //Add more values to formula as more stat types get implemented.
            int damage = this.AttackPower.Value; 
            target.TakeDamage(damage);
        }

        public Npc GetTarget()
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
            this.EnemyPosition = position;
        }

        public void Update(GameTime gameTime)
        {
            this.animation.PlayAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.animation.Draw(spriteBatch, this.Position);
        }
    }
}
