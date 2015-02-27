namespace RPG.GameLogic.Core.Battle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.Xna.Framework.Input;
    using RPG.GameLogic.Interface;
    using RPG.GameLogic.Models.Characters.Base;
    using RPG.GameLogic.Models.Spells.Base;

    class Battle
    {
        private Random rnd = new Random();
        private bool tookTurn = true; //Used for preventing the turn system from selectin a new fighter this turn if the previous one has not finished his turn.
        private int targetIndex = 0; //Index of current target the player is aiming at.
        private int skillIndex = 0; //Index of current Skill that the player has selected.
        private bool leftPressed; //Emulating key pressed functionality in keylistener
        private bool rightPressed; //Emulating key pressed functionality in keylistener
        private bool spacePressed; //Emulating key pressed functionality in keylistener
        private bool targetSelected; //If player has selected a target he may proceed to select a way to attack his target.
        private bool skillSelected; //If player has selected a Skill he casts it and "Enemy go boom sir"
        private string status; //A status text for the battle.
        private Skill skill; //Currently selected Skill.
        private List<int> availableFighterIndexes; //Indexes of all available fighters for the current round.

        /// <summary>
        /// Create a new battle between the player and the enemy and it's surrounding allies
        /// </summary>
        /// <param name="player">The character our player controls</param>
        /// <param name="trigger">The enemy</param>
        public Battle(IPlayer player, List<Character> trigger)
        {
            this.Attacker = player;
            this.Player = player as Character;
            this.CurrentTurn = 1;
            this.Round += 1;
            this.Enemies = trigger;
            this.Target = player as Character;
            this.PlayerState = "   Waiting.";
            this.Status = "Battle initiated";
        }

        /// <summary>
        /// Count of rounds passed.
        /// </summary>
        public int Round { get; private set; }

        /// <summary>
        /// List of all enemies that the player has to kill to win the battle.
        /// </summary>
        public List<Character> Enemies { get; private set; }

        /// <summary>
        /// The character out player controls
        /// </summary>
        public Character Player { get; private set; }

        /// <summary>
        /// The count of turns passed this round.
        /// </summary>
        public int CurrentTurn { get; private set; }

        /// <summary>
        /// If the battle is in progress you can call .NextTurn() to proceed with the fight.
        /// </summary>
        public bool InProgress { get; private set; }

        /// <summary>
        /// A list of every character in the battle player included.
        /// </summary>
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

        public string PlayerState { get; private set; }
        /// <summary>
        /// Amount of turns each round has.
        /// </summary>
        public int TotalTurns
        {
            get { return this.Enemies.Count + 1; }
        }

        /// <summary>
        /// The character that will be attacked next.
        /// </summary>
        public Character Target { get; private set; }

        /// <summary>
        /// The character that will attack next.
        /// </summary>
        public IFight Attacker { get; private set; }

        /// <summary>
        /// A string representation of the current state of the battle. Has a thread sleep of 200 when being set.
        /// </summary>
        public string Status
        {
            get
            {
                return this.status;
            }
            private set
            {
                if (!this.PlayerState.Contains("Select"))
                {
                    Thread.Sleep(300);
                }
                this.status = value;
            }
        }

        /// <summary>
        /// Starts the current battle. Call battle.NextTurn() to proceed with the fight.
        /// </summary>
        public void StartFight()
        {
            this.InProgress = true;
            this.availableFighterIndexes = this.GetFighterIndexes(this.Participants);
        }
        /// <summary>
        /// Proceed with fight.
        /// </summary>
        public void NextTurn()
        {
            // Check if the battle is over. Current conditions are if the player is dead or the enemy count reaches 0;
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

            if (!this.InProgress)
            {
                return;
            }

            // Check if the current fighter has taken it's turn if he will proceed with target selection and attack when one is selected.
            if (this.tookTurn)
            {
                // Every fighter in the battle with take one turn each round.
                this.Attacker = this.SelectFighter(this.Participants, this.availableFighterIndexes);
                this.Status = string.Format("Round: {1:###}.\n{0} is on turn.", ((Character)this.Attacker).Name, this.Round);
                this.tookTurn = false;
            }

            // If the selected attacked is a player he may then pick a target to attack.
            if (this.Attacker is IPlayer)
            {
                if (!this.targetSelected)
                {
                    this.PlayerState = "Select Target!";
                    this.Target = this.ProcessSelection(this.Enemies, ref this.targetIndex, ref this.targetSelected);
                    this.Status = string.Format(
                        "Round: {4:###}.\nCurrent target: {2}\nTarget Health: {3:###} | Target Defense: {5:###} | Target Attack: {6:###}\n Total Targets: {1:###}\n Current Target: {0:###}", this.targetIndex + 1, this.Enemies.Count, this.Target.Name, this.Target.Health.Value, this.Round, this.Target.Defense.Value, ((IFight)this.Target).AttackPower.Value);
                }

                // When the player selects a target he may then select a Skill or a basic attack to fight his target with.
                else if (this.targetSelected && !this.skillSelected)
                {
                    this.PlayerState = "Select Action!";
                    this.skill = this.ProcessSelection(((IPlayer)this.Player).Skills, ref this.skillIndex, ref this.skillSelected);
                    this.Status = string.Format("Round: {4:###}.\nTarget: {0}({1:###}hp/{5:###}def)\nCurrent action: {2}\n Action power: {3:###}", this.Target.Name, this.Target.Health.Value, this.skill.Name, this.skill.Stat.Value, this.Round, this.Target.Defense.Value);
                }

                // When the player has selected a Skill and a target he will take action and the next round will be called.
                else if (this.targetSelected && this.skillSelected)
                {
                    if (this.skill is SelftCastSkill)
                    {
                       ((SelftCastSkill)this.skill).Cast();
                       this.status = string.Format("Round: {0:###}.\n {1} used {2}", this.Round, this.Player.Name, this.skill.Name);
                    }
                    else
                    {
                        ((TargetedSkill)this.skill).Cast(this.Target);
                        this.PlayerState = "Attacking!";
                        string statusString = string.Format("Round: {0:###}.\n{1} is attacking {2} with {3}.", this.Round, ((Character)this.Attacker).Name, this.Target.Name, this.skill.Name);
                        statusString += this.Target.Health.Value == 0 ? string.Format("\n{0} is kill!", this.Target.Name) :
                            string.Format("\n{0} has {1:###}hp left!", this.Target.Name, this.Target.Health.Value);
                        this.status = statusString;
                    }
                    this.tookTurn = true;
                    this.skillSelected = false;
                    this.targetSelected = false;
                    this.CurrentTurn += 1;
                }
            }

            //If en enemy is on turn he will just target the player and hit him with a basic attack.
            else
            {
                this.PlayerState = "   Waiting.";
                this.tookTurn = true;
                this.Target = this.Player;
                this.Attacker.Attack(this.Target as IFight);

                string statusString = string.Format("Round: {0:###}.\n{1} is attacking {2}.", this.Round, ((Character)this.Attacker).Name, this.Target.Name);
                statusString += this.Target.Health.Value == 0 ? string.Format("\n{0} is kill!", this.Target.Name) : string.Format("\n{0} has {1:###}hp left!", this.Target.Name, this.Target.Health.Value);
                this.status = statusString;

                this.CurrentTurn += 1;
            }
            // After every turn we clear the bodies and pay our respects to the dead.
            this.ClearBattlefield();

            // Check if it's time to start a new round.
            if (this.CurrentTurn >= this.TotalTurns)
            {
                this.CurrentTurn = 1;
                this.Round += 1;
                this.status = string.Format("Preparing round {0:###}.", this.Round);
                // If it passes the available fighters are reset and shuffled.
                this.availableFighterIndexes = this.GetFighterIndexes(this.Participants);
                // When castins some skills the player will put an effect on his enemies.
                // When the round ends the effects will take effect and be applied once to each enemy.
                
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
                

            }
        }

        /// <summary>
        /// Select a target from a list of enemies.
        /// </summary>
        /// <typeparam name="T">Type of selection.</typeparam>
        /// <param name="selections">List of targets to select from.</param>
        /// <param name="index">Index of currently selected target.</param>
        /// <param name="selected">A boolean value representing the current stated of the selection.</param>
        /// <returns>Returns selected target.</returns>
        public T ProcessSelection<T>(List<T> selections, ref int index, ref bool selected)
        {
            var ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                if (!this.leftPressed)
                {
                    this.SelectLeft(ref index, selections);
                }
                this.leftPressed = true;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                if (!this.rightPressed)
                {
                    this.SelectRight(ref index, selections);
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
            return this.Select(index, selections);
        }

        /// <summary>
        /// Select the first index of the list and return the corresponding fighter while removing the index from the list.
        /// </summary>
        /// <param name="fighters">A list of fighters participating in the selection</param>
        /// <param name="availableIndexes">The indexes of all the fighters in the selection</param>
        /// <returns>Returns a fighter from the list.</returns>
        private IFight SelectFighter(List<Character> fighters, List<int> availableIndexes)
        {
            var index = availableIndexes[0];
            availableIndexes.Remove(index);
            var fighter = fighters[index];
            return fighter as IFight;
        }

        /// <summary>
        /// Clear all the dead enemies from the battlefield.
        /// </summary>
        private void ClearBattlefield()
        {
            this.Enemies.RemoveAll(enemy => enemy.Health.Value == 0);
        }

        /// <summary>
        /// Restart the index if it overflows the list.
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="index">Current index.</param>
        /// <param name="targets">List of targets.</param>
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

        /// <summary>
        /// Reduce index by one.
        /// </summary>
        /// <param name="index">Current index.</param>
        /// <param name="targets">List of available targets</param>
        public void SelectLeft<T>(ref int index, List<T> targets)
        {
            index -= 1;
            this.AdjustIndex(ref index, targets);
        }
        /// <summary>
        /// Increase index by one.
        /// </summary>
        /// <param name="index">Current index.</param>
        /// <param name="targets">List of available targets</param>
        private void SelectRight<T>(ref int index, List<T> targets)
        {
            index += 1;
            this.AdjustIndex(ref index, targets);
        }

        /// <summary>
        /// Select the indexed target from the list.
        /// </summary>
        /// <typeparam name="T">Type of target</typeparam>
        /// <param name="index">Index of target</param>
        /// <param name="targets">List of targets</param>
        /// <returns>Returns selected target from the list</returns>
        private T Select<T>(int index, List<T> targets)
        {
            this.AdjustIndex(ref index, targets);
            return targets[index];
        }

        /// <summary>
        /// Get a list of all indexes that belong the given list of participants.
        /// </summary>
        /// <param name="participants"></param>
        /// <returns>Returns a list containing an index for each participant.</returns>
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
