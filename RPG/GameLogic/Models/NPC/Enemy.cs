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

        public Enemy(string id, string name, int health,
            int attack, int defense, List<Stat> otherStats)
            : base(id, name, health, defense)
        {
            this.AttackPower = new Attack(attack);
            this.Stats = otherStats;
            this.Animation = new Animation("Sprites\\Monster\\enemy_new", 80f, 2, 4, false, 0, 0);
        }

        public Enemy(string id, string name, int health,
            int attack, int defense)
            : this(id, name, health, attack, defense, null)
        {
        }

        public Animation Animation { get; private set; }

        public Texture2D EnemyTexture { get; set; }

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
            this.Position = position;
        }

        public void Update(GameTime gameTime)
        {
            this.Animation.PlayAnimation(gameTime);
            this.Animation.PlayAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Animation.Draw(spriteBatch, this.Position);
            this.Animation.Draw(spriteBatch, this.Position);
        }
    }
}