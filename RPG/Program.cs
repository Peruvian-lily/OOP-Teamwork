﻿#region Using Statements
using System;
using Microsoft.Xna.Framework;
using RPG.GameLogic.Core.Items;
using RPG.GameLogic.Models.Items;

#endregion

namespace RPG
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GameStateManagementGame())
                game.Run();
        }
    }
#endif
}
