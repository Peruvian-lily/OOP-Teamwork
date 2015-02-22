using System;
using System.Collections.Generic;
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
        private bool leftPressed;
        private bool rightPressed;

        public Battle(IPlayer player, List<Character> trigger)
        {
            this.Attacker = player;
            this.Player = player;
            this.CurrentTurn = 1;
            this.Enemies = trigger;
            this.Target = player;
            this.AddParticipants();
        }

        public int Round { get; private set; }
        public List<Character> Enemies { get; private set; }
        public IPlayer Player { get; private set; }
        public int CurrentTurn { get; private set; }
        public bool InProgress { get; private set; }
        public int TotalTurns
        {
            get { return this.Participants.Count; }
        }
        public IFight Target { get; private set; }
        public IFight Attacker { get; private set; }
        public List<Character> Participants { get; private set; }

        public void StartFight()
        {
            this.InProgress = true;
        }
        public void NextTurn()
        {
            if (!this.Participants.Contains(this.Player as Character) || this.Participants.Count == 1)
            {
                this.InProgress = false;
            }

            if (!this.InProgress) return;
            var participants = this.Participants.ToList();

            if (tookTurn)
            {
                this.Attacker = this.SelectFighter(participants);
                participants.Remove(this.Attacker as Character);
                this.tookTurn = false;
            }
            if (this.Attacker is IPlayer)
            {
                bool selected = false;
                ProcessTargetting(participants, ref selected);
                if (selected)
                {
                    this.tookTurn = true;
                    this.Attacker.Attack(this.Target);
                    this.CurrentTurn += 1;
                }
            }
            else
            {
                this.tookTurn = true;
                this.Target = this.Player;
                this.Attacker.Attack(this.Target);
                this.CurrentTurn += 1;
            }
            this.ClearBattlefield();

            if (CurrentTurn >= this.TotalTurns)
            {
                this.CurrentTurn = 1;
                this.Round += 1;
            }
        }
        private void AddParticipants()
        {
            this.Participants = new List<Character>
            {
                this.Player as Character
            };
            this.Participants.AddRange(this.Enemies);
        }

        private IFight SelectFighter(List<Character> participants)
        {
            var onTurn = participants[rnd.Next(0, participants.Count)];
            return onTurn as IFight;
        }

        private void ClearBattlefield()
        {
            this.Participants.RemoveAll(participant => participant.Health.Value == 0);
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
