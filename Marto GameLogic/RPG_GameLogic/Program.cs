using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_GameLogic.Stats;
using RPG_GameLogic.Items;
using RPG_GameLogic.Factories;
using RPG_GameLogic.Enums;

namespace RPG_GameLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            Currency currOne = new Currency(1, 0, 1);
            Currency currTwo = new Currency(0, 0, 1);

            Console.WriteLine(currOne > currTwo);
        }
    }
}
