﻿namespace UnitTestProject1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RPG.GameLogic.Models.Characters;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSpellCreation()
        {
            Player pesho = new Player("100", 100, 100, 100, 5);
            Assert.AreEqual(100, pesho.AttackPower.Value);
        }
    }
}
