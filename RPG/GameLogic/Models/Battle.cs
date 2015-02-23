using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;

namespace RPG.GameLogic.Models
{
    class Battle
    {
        private Random rnd = new Random();
        private bool tookTurn = true;
        private int targetIndex;
        private List<int> targetIndexes;
        private bool leftPressed;
        private bool rightPressed;
        private List<Character> enemies;
        private IFight attacker;

        public Battle(Player player, List<Character> trigger)
        {
            this.attacker = player;
            this.Player = player;
            this.CurrentTurn = 1;
            this.enemies = trigger;
            this.Target = player;
        }

        public int Round { get; private set; }

        public List<Character> Enemies
        {
            get
            {
                return this.enemies;
            }
        }

        public IPlayer Player { get; private set; }
        public int CurrentTurn { get; private set; }
        public bool InProgress { get; private set; }
        public int TotalTurns
        {
            get { return this.enemies.Count + 1; }
        }
        public IFight Target { get; private set; }
        public IFight Attacker
        {
            get { return this.attacker; }
        }

        public string Status { get; private set; }

        public void StartFight()
        {
            this.InProgress = true;
        }
        public void NextTurn()
        {
            if (this.Player.Health.Value == 0)
            {
                this.Status = string.Format("{0} is kill. :(", (this.Player as Character).Name);
                this.InProgress = false;
            }else if (this.enemies.Count == 0)
            {
                this.Status = string.Format("{0} killed all the enemies!", ((Character)Player).Name);
                this.InProgress = false;
            }

            if (!this.InProgress) return;
            if (tookTurn)
            {
                this.attacker = rnd.Next(5) > 2 ? this.SelectFighter(this.enemies) : this.Player;
                this.Status = string.Format("{0} is on turn.", ((Character)this.attacker).Name);
                this.tookTurn = false;
            }
            if (this.attacker is IPlayer)
            {
                bool selected = false;
                ProcessTargetting(this.enemies, ref selected);
                this.Status = string.Format("Current target: {0}", ((Character)this.Target).Name);
                if (selected)
                {
                    this.tookTurn = true;
                    this.Status = string.Format("{0} is attacking {1}({2}).", 
                        ((Character)this.Player).Name, ((Character)this.Target).Name,
                    this.Target.Health);
                    this.attacker.Attack(this.Target);
                    this.CurrentTurn += 1;
                }
            }
            else
            {
                this.tookTurn = true;
                this.Target = this.Player;
                this.attacker.Attack(this.Target);
                this.CurrentTurn += 1;
            }
            this.ClearBattlefield();

            if (CurrentTurn >= this.TotalTurns)
            {
                this.CurrentTurn = 1;
                this.Round += 1;
            }
        }

        private IFight SelectFighter(List<Character> participants)
        {
            var onTurn = participants[rnd.Next(0, participants.Count)];
            return onTurn as IFight;
        }

        private void ClearBattlefield()
        {
            this.Enemies.RemoveAll(enemy => enemy.Health.Value == 0);
        }
        public void ProcessTargetting(List<Character> participants, ref bool selected)
        {
            var ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                if (!leftPressed)
                {
                    ChangeIndex("reduce", participants);
                }
                leftPressed = true;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                if (!rightPressed)
                {
                    ChangeIndex("increase", participants);
                }
                rightPressed = true;
            }
            else if (ks.IsKeyDown(Keys.Space))
            {
                selected = true;
            }
            if (ks.IsKeyUp(Keys.Left))
            {
                leftPressed = false;
            }
            if (ks.IsKeyUp(Keys.Right))
            {
                rightPressed = false;
            }
            AdjustIndex(participants);
            this.Target = participants[targetIndex] as IFight;
        }
        private void ChangeIndex(string action, List<Character> targets)
        {
            switch (action.ToLower())
            {
                case "reduce":
                    targetIndex -= 1;
                    break;
                case "increase":
                    targetIndex += 1;
                    break;
            }
        }

        private void AdjustIndex(List<Character> targets)
        {
            if (targetIndex < 0)
            {
                targetIndex = targets.Count() - 1;
            }
            else if (targetIndex > targets.Count - 1)
            {
                targetIndex = 0;
            }
        }
    }
}
