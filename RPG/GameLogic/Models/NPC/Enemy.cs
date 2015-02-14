﻿using System.Collections.Generic;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

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
        }

        public Enemy(string id, string name, int health,
            int attack, int defense)
            : this(id, name, health, attack, defense, null)
        {
        }

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
    }
}
