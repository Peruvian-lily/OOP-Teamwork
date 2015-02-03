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
            Weapon item = ItemFactory.CreateWearableItemWithDefaultValues<Weapon>();
            Console.WriteLine(item);
        }
    }
}
