using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPG_GameLogic;

namespace RPG_GameLogicTests
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod]
        public void NegativeValuesInConsturctor()
        {
            bool exceptionThrown = false;

            try
            {
                Currency first = new Currency(0, 0, -1);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.AreEqual(true, exceptionThrown);
            exceptionThrown = false;

            try
            {
                Currency first = new Currency(0, -1, 0);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.AreEqual(true, exceptionThrown);
            exceptionThrown = false;

            try
            {
                Currency first = new Currency(-1, 0, 0);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.AreEqual(true, exceptionThrown);
        }

        [TestMethod]
        public void AdditionTests() 
        {
            Currency first = new Currency(0, 0, 99);
            Currency second = new Currency(0, 0, 1);
            Currency expected = new Currency(0, 1, 0);

            Assert.AreEqual(expected, first + second);

            first = first + second;
            second = new Currency(0, 99, 0);
            expected = new Currency(1, 0, 0);

            Assert.AreEqual(expected, first + second);

            first = new Currency(0, 99, 99);
            second = new Currency(1, 1, 1);
            expected = new Currency(2, 1, 0);

            Assert.AreEqual(expected, first + second);

            first = new Currency(0, 0, 0);
            second = new Currency(0, 0, 50);
            expected = new Currency(0, 7, 0);
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;
            first = first + second;

            Assert.AreEqual(expected, first);
        }

        [TestMethod]
        public void SubtractionTests()
        {
            Currency first = new Currency(0, 1, 1);
            Currency second = new Currency(0, 1, 0);
            Currency expected = new Currency(0, 0, 1);

            Assert.AreEqual(expected, first - second);

            first = new Currency(0, 99, 1);
            second = new Currency(1, 1, 0);
            bool exceptionThrown = false;

            try
            {
                new Currency(first - second);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.AreEqual(true, exceptionThrown);

            first = new Currency(0, 0, 99);
            second = new Currency(0, 1, 0);
            exceptionThrown = false;

            try
            {
                new Currency(first - second);
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }

            Assert.AreEqual(true, exceptionThrown);

            first = new Currency(1, 0, 0);
            second = new Currency(0, 99, 99);
            expected = new Currency(0, 0, 1);

            Assert.AreEqual(expected, first - second);

            first = new Currency(1, 0, 0);
            second = new Currency(0, 99, 1);
            expected = new Currency(0, 0, 99);

            Assert.AreEqual(expected, first - second);

            first = new Currency(1, 0, 0);
            second = new Currency(0, 0, 1);
            expected = new Currency(0, 99, 99);

            Assert.AreEqual(expected, first - second);

            first = new Currency(1, 99, 0);
            second = new Currency(0, 99, 99);
            expected = new Currency(0, 99, 1);

            Assert.AreEqual(expected, first - second);
        }
    }
}
