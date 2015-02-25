using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Characters.Base;

namespace RPG.GameLogic.Core.Battle
{
  class Battle
    {
        private Random rnd = new Random();
        private bool tookTurn = true;
        private int targetIndex;
        private List<int> targetIndexes;
        private bool leftPressed;
        private bool rightPressed;
        private string status;

        public Battle(IPlayer player, List<Character> trigger)
        {
            this.Attacker = player;
            this.Player = player;
            this.CurrentTurn = 1;
            this.Enemies = trigger;
            this.Target = player;
            this.Status = "BattleScreenState initiated";
        }

        public int Round { get; private set; }
        public List<Character> Enemies { get; private set; }
        public IPlayer Player { get; private set; }
        public int CurrentTurn { get; private set; }
        public bool InProgress { get; private set; }
        public int TotalTurns
        {
            get { return this.Enemies.Count + 1; }
        }
        public IFight Target { get; private set; }
        public IFight Attacker { get; private set; }

        public string Status
        {
            get
            {
                return this.status;
            }
            private set
            {
                if (!value.Contains("Select target"))
                {
                    Thread.Sleep(300);
                }
                this.status = value;
            }
        }

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
            }
            else if (this.Enemies.Count == 0)
            {
                this.Status = string.Format("{0} killed all the enemies!", ((Character)this.Player).Name);
                this.InProgress = false;
            }

            if (!this.InProgress) return;
            if (this.tookTurn)
            {
                this.Attacker = this.rnd.Next(5) > 2 ? this.SelectFighter(this.Enemies) : this.Player;
                this.Status = string.Format("{0} is on turn.", ((Character)this.Attacker).Name);
                this.tookTurn = false;
            }
            if (this.Attacker is IPlayer)
            {
                bool selected = false;
                this.ProcessTargetting(this.Enemies, ref selected);
                this.Status = string.Format("Select target({0}): {1}({2} hp)", 
                    this.Enemies.Count, ((Character)this.Target).Name, this.Target.Health.Value);
                if (selected)
                {
                    this.tookTurn = true;
                    this.Status = string.Format("{0} is attacking {1}({2} hp).",
                        ((Character)this.Attacker).Name, ((Character)this.Target).Name, this.Target.Health.Value);
                    this.Attacker.Attack(this.Target);
                    this.CurrentTurn += 1;
                }
            }
            else
            {
                this.tookTurn = true;
                this.Target = this.Player;
                this.Status = string.Format("{0} is attacking {1}({2} hp).",
                     ((Character)this.Attacker).Name, ((Character)this.Target).Name, this.Target.Health.Value);
                Thread.Sleep(50);
                this.Attacker.Attack(this.Target);
                this.CurrentTurn += 1;
            }
            this.ClearBattlefield();

            if (this.CurrentTurn >= this.TotalTurns)
            {
                this.CurrentTurn = 1;
                this.Round += 1;
            }
        }

        private IFight SelectFighter(List<Character> participants)
        {
            var onTurn = participants[this.rnd.Next(0, participants.Count)];
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
                if (!this.leftPressed)
                {
                    this.ChangeIndex("reduce", participants);
                }
                this.leftPressed = true;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                if (!this.rightPressed)
                {
                    this.ChangeIndex("increase", participants);
                }
                this.rightPressed = true;
            }
            else if (ks.IsKeyDown(Keys.Space))
            {
                selected = true;
            }
            if (ks.IsKeyUp(Keys.Left))
            {
                this.leftPressed = false;
            }
            if (ks.IsKeyUp(Keys.Right))
            {
                this.rightPressed = false;
            }
            this.AdjustIndex(participants);
            this.Target = participants[this.targetIndex] as IFight;
        }
        private void ChangeIndex(string action, List<Character> targets)
        {
            switch (action.ToLower())
            {
                case "reduce":
                    this.targetIndex -= 1;
                    break;
                case "increase":
                    this.targetIndex += 1;
                    break;
            }
        }

        private void AdjustIndex(List<Character> targets)
        {
            if (this.targetIndex < 0)
            {
                this.targetIndex = targets.Count() - 1;
            }
            else if (this.targetIndex > targets.Count - 1)
            {
                this.targetIndex = 0;
            }
        }
    }
}
