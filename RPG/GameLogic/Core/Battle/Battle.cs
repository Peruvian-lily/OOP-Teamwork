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

        public Battle(IPlayer player, List<Character> trigger)
        {
            this.Attacker = player;
            this.Player = player;
            this.CurrentTurn = 1;
            this.Round += 1;
            this.Enemies = trigger;
            this.Target = player as Character;
            this.Status = "Battle initiated";
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
                    Thread.Sleep(100);
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
                this.Status = string.Format("{0} is kill. :(", ((Character)this.Player).Name);
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
                this.Attacker = this.rnd.Next(5) > 1 ? this.SelectFighter(this.Enemies) : this.Player;
                this.Status = string.Format("{0} is on turn.", ((Character)this.Attacker).Name);
                this.tookTurn = false;
            }
            if (this.Attacker is IPlayer)
            {
                if (!targetSelected)
                {
                    this.Target = ProcessSelection(this.Enemies, ref this.targetIndex, ref this.targetSelected);
                    this.Status = string.Format("Select target({0}/{1}): {2}({3}hp)",
                        this.Enemies.Count, this.targetIndex+1, this.Target.Name, this.Target.Health.Value);
                }
                else if (targetSelected && !spellSelected)
                {
                    this.spell = ProcessSelection(this.Player.Spells, ref this.spellIndex, ref this.spellSelected);
                    this.Status = string.Format("Target {0}({1}hp):\nSelect attack: {2} (Power:{3})", this.Target.Name, this.Target.Health.Value, this.spell.Name, this.spell.Stat.Value);
                }
                else if (this.targetSelected && spellSelected)
                {
                    this.status = string.Format("{0} is attacking {1} with {2}", ((Character)this.Attacker).Name, this.Target.Name, this.spell.Name);
                    this.spell.Cast(this.Target);
                    this.tookTurn = true;
                    this.spellSelected = false;
                    this.targetSelected = false;
                    this.CurrentTurn += 1;
                }
            }
            else
            {
                this.tookTurn = true;
                this.Target = (Character) this.Player;
                this.Status = string.Format("{0} is attacking {1}({2} hp).",
                     ((Character)this.Attacker).Name, this.Target.Name, this.Target.Health.Value);
                this.Attacker.Attack(this.Target as IFight);
                this.CurrentTurn += 1;
            }
            this.ClearBattlefield();

            if (this.CurrentTurn >= this.TotalTurns)
            {
                this.status = string.Format("Beginning round #{0}", this.Round);
                  this.Enemies.Where(enemy => enemy.Effects.Count > 0).ToList().ForEach(enemy =>
                  {
                      enemy.Effects.ForEach(effect => effect.Tick(enemy));
                  });
                this.Player.Effects.ForEach(effect => effect.Tick(this.Player as Character));
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
    }
}
