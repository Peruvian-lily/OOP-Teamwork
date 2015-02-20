using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace RPG.Helpers
{  

    public class InputState
    {
        #region Fields

        public readonly KeyboardState CurrentKeyboardStates;

        public KeyboardState LastKeyboardStates;


        public TouchCollection TouchState;

        public readonly List<GestureSample> Gestures = new List<GestureSample>();

        #endregion

        #region Initialization



        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState();

            LastKeyboardStates = new KeyboardState();

        }


        #endregion

        #region Public Methods



        public void Update()
        {
          
                LastKeyboardStates = CurrentKeyboardStates;
            

            TouchState = TouchPanel.GetState();

            Gestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                Gestures.Add(TouchPanel.ReadGesture());
            }
        }



        public bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer,
                                            out PlayerIndex playerIndex)
        {


            playerIndex = controllingPlayer.GetValueOrDefault();

            int i = (int)playerIndex;

            return (CurrentKeyboardStates.IsKeyDown(key) &&
                    LastKeyboardStates.IsKeyUp(key));
      

        }


        /// <summary>
        /// Helper for checking if a button was newly pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>



        /// <summary>
        /// Checks for a "menu select" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuSelect(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex) ||
                   IsNewKeyPress(Keys.Enter, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu cancel" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuCancel(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu up" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu down" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Checks for a "pause the game" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsPauseGame(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex);
        }


        #endregion
    }
}
