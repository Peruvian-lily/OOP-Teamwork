using System;
using System.Collections.Generic;
using System.Linq;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Characters.Base;

namespace RPG.GameLogic.Models
{
    class Battle
    {
        private static Random rnd = new Random();

        public Battle(IPlayer player, IEnemy trigger)
        {
            this.Enemies = trigger.GetAllies();
            this.Player = player;
            this.TotalTurns = trigger.GetAllies().Count + 1;
            this.CurrentTurn = 1;
            this.AddParticipants();
        }

        public int Round { get; private set; }
        public List<IFight> Enemies { get; private set; }
        public IPlayer Player { get; private set; }
        public int CurrentTurn { get; private set; }
        public int TotalTurns { get; private set; }
        public List<Character> Participants { get; private set; }

        private void AddParticipants()
        {
            this.Participants = new List<Character>();
            this.Participants.Add(this.Player as Character);
            this.Enemies.ForEach(enemy =>
            {
                this.Participants.Add(enemy as Character);
            });
        }

        private void StartFight()
        {
            var participants = this.Participants.ToList();
            while (this.CurrentTurn <= this.TotalTurns)
            {
                // Fighter represents the current character on turn.
                var fighter = this.OnTurn(ref participants);
                var target = fighter is IPlayer ? this.SelectTarget() : this.Player;
                this.Fight(fighter, target);
                this.ClearBattlefield();
                this.CurrentTurn += 1;
            }
            this.CurrentTurn = 1;
            this.Round += 1;
        }
        private IFight OnTurn(ref List<Character> participants)
        {
            var onTurn = participants[rnd.Next(0, participants.Count)];
            participants.Remove(onTurn);
            return onTurn as IFight;
        }

        private void Fight(IFight attacker, IFight defender)
        {
            attacker.Attack(defender as Character);
        }

        private void ClearBattlefield()
        {
            this.Participants.RemoveAll(participant => participant.Health.Value <= 0);
        }

        /// <summary>
        /// Player selects target.
        /// </summary>
        /// <returns>Returns the target the player has selected.</returns>
        private IFight SelectTarget()
        {
            throw new NotImplementedException();
        }
    }
}
