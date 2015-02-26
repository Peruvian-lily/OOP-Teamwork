using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Characters;
using RPG.GameLogic.Models.Characters.Base;
using RPG.GameLogic.Models.Spells.Base;

namespace RPG.GameLogic.Core.Battle
{
    class Battle
    {
        private Random rnd = new Random();
        private bool tookTurn = true;
        private int targetIndex = 0;
        private int spellIndex = 0;
        private List<int> targetIndexes;
        private bool leftPressed;
        private bool rightPressed;
        private bool spacePressed;
        private bool targetSelected;
        private bool spellSelected;
        private string status;
        private Spell spell;
        private List<int> availableFighterIndexes;

        public Battle(IPlayer player, List<Character> trigger)
        {
            this.Attacker = player;
            this.Player = player as Character;
            this.CurrentTurn = 1;
            this.Round += 1;
            this.Enemies = trigger;
            this.Target = player as Character;
            this.Status = "Battle initiated";
        }

        public int Round { get; private set; }
        public List<Character> Enemies { get; private set; }
        public Character Player { get; private set; }
        public int CurrentTurn { get; private set; }
        public bool InProgress { get; private set; }
        public List<Character> Participants
        {
            get
            {
                return new List<Character>(this.Enemies)
                {
                    this.Player
                };
            }
        }
        public int TotalTurns
        {
            get { return this.Enemies.Count + 1; }
        }
        public Character Target { get; private set; }
        public IFight Attacker { get; private set; }

        public string Status
        {
            get
            {
                return this.status;
            }
            private set
            {
                if (!value.Contains("Select"))
                {
                    Thread.Sleep(50);
                }
                this.status = value;
            }
        }

        public void StartFight()
        {
            this.InProgress = true;
            this.availableFighterIndexes = GetFighterIndexes(this.Participants);
        }
        public void NextTurn()
        {
            if (this.Player.Health.Value == 0)
            {
                this.Status = string.Format("{0} is kill. :(", ((Character)this.Player).Name);
                this.InProgress = false;
            }
            else if (this.Enemies.Count == 0)
            {
                this.Status = string.Format("{1}|{0} killed all the enemies!", ((Character)this.Player).Name, this.Round);
                this.InProgress = false;
            }

            if (!this.InProgress) return;
            if (this.tookTurn)
            {
                this.Attacker = this.SelectFighter(this.Participants, this.availableFighterIndexes);
                this.Status = string.Format("{1}|{0} is on turn.", ((Character)this.Attacker).Name, this.Round);
                this.tookTurn = false;
            }
            if (this.Attacker is IPlayer)
            {
                if (!targetSelected)
                {
                    this.Target = ProcessSelection(this.Enemies, ref this.targetIndex, ref this.targetSelected);
                    this.Status = string.Format("{4}|Select target({0}/{1}): {2}({3}hp)",
                        this.Enemies.Count, this.targetIndex + 1, this.Target.Name, this.Target.Health.Value, this.Round);
                }
                else if (targetSelected && !spellSelected)
                {
                    this.spell = ProcessSelection(((IPlayer)this.Player).Spells, ref this.spellIndex, ref this.spellSelected);
                    this.Status = string.Format("{4}|Target {0}({1}hp):\nSelect attack: {2} (Power:{3})",
                        this.Target.Name, this.Target.Health.Value, this.spell.Name, this.spell.Stat.Value, this.Round);
                }
                else if (this.targetSelected && spellSelected)
                {
                    this.spell.Cast(this.Target);
                    this.status = string.Format("{4}| {0} is attacking {1} with {2}\n{1} has {3}hp left",
                        ((Character)this.Attacker).Name, this.Target.Name, this.spell.Name, this.Target.Health.Value, this.Round);
                    this.tookTurn = true;
                    this.spellSelected = false;
                    this.targetSelected = false;
                    this.CurrentTurn += 1;
                }
            }
            else
            {
                this.tookTurn = true;
                this.Target = this.Player;
                this.Attacker.Attack(this.Target as IFight);
                this.Status = string.Format("{3}| {0} is attacking {1}.\n{1} has ({2} hp) left",
                    ((Character)this.Attacker).Name, this.Target.Name, this.Target.Health.Value, this.Round);
                this.CurrentTurn += 1;
            }
            this.ClearBattlefield();

            if (this.CurrentTurn >= this.TotalTurns)
            {
                this.availableFighterIndexes = GetFighterIndexes(this.Participants);
                this.status = string.Format("Beginning of round #{0}", this.Round);
                this.Enemies.Where(enemy => enemy.Effects.Count > 0).ToList()
                    .ForEach(enemy =>
                    {
                    enemy.Effects
                        .ForEach(effect =>
                        {
                            effect.Tick(enemy);
                            //this.status = string.Format("{0} takes {1} damage from {2}", enemy.Name, effect.Stat.Value, effect);
                        });
                    });
                this.Player.Effects.ForEach(effect =>
                {
                    effect.Tick(this.Player);
                    //this.status = string.Format("{0} takes {1} damage from {2}", this.Player.Name, effect.Stat.Value, effect);
                });
                this.CurrentTurn = 1;
                this.Round += 1;
            }
        }

        private IFight SelectFighter(List<Character> participants, List<int> availableIndexes)
        {
            var index = availableIndexes[0];
            availableIndexes.Remove(index);
            var fighter = participants[index];
            return fighter as IFight;
        }

        private void ClearBattlefield()
        {
            this.Enemies.RemoveAll(enemy => enemy.Health.Value == 0);
        }

        public T ProcessSelection<T>(List<T> selections, ref int index, ref bool selected)
        {
            var ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                if (!this.leftPressed)
                {
                    index -= 1;
                }
                this.leftPressed = true;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                if (!this.rightPressed)
                {
                    index += 1;
                }
                this.rightPressed = true;
            }
            else if (ks.IsKeyDown(Keys.Space))
            {
                if (!this.spacePressed)
                {
                    selected = true;
                }
                this.spacePressed = true;
            }
            if (ks.IsKeyUp(Keys.Left))
            {
                this.leftPressed = false;
            }
            if (ks.IsKeyUp(Keys.Right))
            {
                this.rightPressed = false;
            }
            if (ks.IsKeyUp(Keys.Space))
            {
                this.spacePressed = false;
            }
            this.AdjustIndex(ref index, selections);
            return selections[index];
        }

        private void AdjustIndex<T>(ref int index, List<T> targets)
        {
            if (index < 0)
            {
                index = targets.Count() - 1;
            }
            else if (index > targets.Count - 1)
            {
                index = 0;
            }
        }

        private List<int> GetFighterIndexes(List<Character> participants)
        {
            var fighterIndexes = new List<int>();
            for (int i = 0; i < participants.Count; i++)
            {
                fighterIndexes.Add(i);
            }
            return fighterIndexes.OrderBy(random => this.rnd.Next()).ToList();
        }
    }
}
