using System;
using System.Collections.Generic;
using System.Text;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Effects.Base;
using RPG.GameLogic.Models.PickUps;
using RPG.GameLogic.Models.Stats;
using RPG.GameLogic.Models.Stats.Base;

namespace RPG.GameLogic.Core.Factory
{
    public static class ItemFactory
    {
        private static Random _rnd = new Random();

        public static Item GenerateItem(int minStat, int maxStat, IFight owner = null)
        {
            var itemType = (ItemType)_rnd.Next(1);
            int power = _rnd.Next(50);
            bool hasEffect = _rnd.Next(5) == 1;

            List<Stat> stats = GenerateStats(power, itemType);
            //Effects effect = GenerateEffect(power,hasEffect);
            //string name = hasEffect ? GenerateName(itemType, effect) : GenerateName(itemType);
            //Item item = new Item(name,stats,effect);
            string name = GenerateName(itemType);
            Item item = new Item(name,stats, null);

            return item;
        }

        #region Stats Generator
        private static List<Stat> GenerateStats(int power, ItemType type)
        {
            List<Stat> stats;
            List<Stat> returnStats = new List<Stat>(2);
            if (type == ItemType.Offensive)
            {
                stats = new List<Stat>
                {
                    new Attack(_rnd.Next(power)+1),
                    new Elemental(_rnd.Next(power)+1, StatType.Offensive)
                };
            }
            else
            {
                stats = new List<Stat>
                {
                    new Elemental(_rnd.Next(power)+1, StatType.Defensive),
                    new Health(_rnd.Next(power)+1),
                    new Defense(_rnd.Next(power)+1),
                };
            }

            int randomStatIndex1 = _rnd.Next(stats.Count);
            int randomStatIndex2 = _rnd.Next(stats.Count);
            //Prevents adding same stat twice. Or at least the index it selects the items by.
            while (randomStatIndex2 == randomStatIndex1)
            {
                randomStatIndex2 = _rnd.Next(stats.Count);
            }
            returnStats.Add(stats[randomStatIndex1]);
            returnStats.Add(stats[randomStatIndex2]);
            return returnStats;
        }
        #endregion Stats Generator

        //#region Effect Generator
        //private static Effects GenerateEffect(int power, bool hasEffect)
        //{
        //    if (!hasEffect)
        //    {
        //        return null;
        //    }
        //    int effectPower = _rnd.Next(power);
        //    EffectTarget target = _rnd.Next(1) == 0 ? EffectTarget.Self : EffectTarget.Others;
        //    if (_rnd.Next(1) == 0)
        //    {
        //        return NewDefensiveEffect(effectPower, target);
        //    }
        //    else
        //    {
        //        return NewOffensiveEffect(effectPower, target);
        //    }
        //}
        //private static Effects NewDefensiveEffect(int power, EffectTarget target)
        //{
        //    //Filthy little effectses. THEY STOLE THE PRECIOUS
        //    var effectses = new List<Effects>
        //    {
        //        new Healing(power+1,target)
        //    };
        //    int randomIndex = _rnd.Next(effectses.Count - 1);
        //    return effectses[randomIndex];
        //}
        //private static Effects NewOffensiveEffect(int power, EffectTarget target)
        //{
        //    var effectses = new List<Effects>
        //    {
        //        new Burning(power+1,target)
        //    };
        //    int randomIndex = _rnd.Next(effectses.Count - 1);
        //    return effectses[randomIndex];
        //}
        //#endregion Effect Generator

        #region Name Generator
        private static string GenerateName(ItemType itemType)
        {
            List<string> name;
            List<string> prefix = new List<string>
            {
                "Cool",
                "Awesome",
                "Cute",
                "Deadly"
            };

            switch (itemType)
            {
                case ItemType.Offensive:
                    name = new List<string>
                    {
                        "Stick", 
                        "Sword",
                        "Frying Pan"
                    };
                    break;
                case ItemType.Deffensive:
                    name = new List<string>
                    {
                        "Shield",
                        "Baby"
                    };
                    break;
                default:
                    name = new List<string>
                    {
                        "Item"
                    };
                    break;
            }
            int prefixIndex = _rnd.Next(prefix.Count);
            int nameIndex = _rnd.Next(name.Count);

            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", prefix[prefixIndex], name[nameIndex]);
            return sb.ToString();
        }
        private static string GenerateName(ItemType itemType, Effects effect)
        {
            List<string> name;
            List<string> suffix;
            List<string> prefix = new List<string>
            {
                "Cool",
                "Awesome",
                "Cute",
                "Deadly"
            };

            switch (itemType)
            {
                case ItemType.Offensive:
                    name = new List<string>
                    {
                        "Stick", 
                        "Sword",
                        "Frying Pan"
                    };
                    break;
                case ItemType.Deffensive:
                    name = new List<string>
                    {
                        "Shield",
                        "Baby"
                    };
                    break;
                default:
                    name = new List<string>
                    {
                        "Item"
                    };
                    break;
            }

            switch (effect.EffectType)
            {
                case EffectType.Harmful:
                    suffix = new List<string>
                    {
                        "of Pain",
                        "of Hurt",
                    };
                    break;

                case EffectType.Helpful:
                    suffix = new List<string>
                    {
                        "of Puppies",
                        "of The Gods"
                    };
                    break;
                default:
                    suffix = new List<string>
                    {
                        "of Something"
                    };
                    break;
            }
            int prefixIndex = _rnd.Next(prefix.Count);
            int nameIndex = _rnd.Next(name.Count);
            int suffixIndex = _rnd.Next(suffix.Count);
            var sb = new StringBuilder();

            sb.AppendFormat("{0} {1} {2}", prefix[prefixIndex],
                name[nameIndex], suffix[suffixIndex]);
            return sb.ToString();
        }
        #endregion Name Generator
    }
}
