

using System.Text;
using RPG.GameLogic.Interface;
using RPG.GameLogic.Models.Effects.Base;
using RPG.GameLogic.Models.NPC.Base;
using RPG.GameLogic.Models.Stats;

namespace RPG.GameLogic.Core.Items
{
    using System;
    using System.Collections.Generic;
    using Models.Effects;
    using Models.Items.Base;
    using Models.Stats.Base;
    public static class ItemFactory
    {
        private static string _id = "";
        private static Random _rnd = new Random();

        public static PickUp GeneratePickUp()
        {
            var itemType = (ItemType) _rnd.Next(1);
            Effects effect = null;
            //At the moment it has a 20% chance to add an effect to the item.
            bool hasEffect = _rnd.Next(5) == 1;
            if (hasEffect)
            {
                
            }
            return null;
        }

        private static void AddEffect(int power)
        {
            EffectTarget target = _rnd.Next(1) == 0 ? EffectTarget.Self : EffectTarget.Others;
            if (_rnd.Next(1) == 0)
            {
                NewDefensiveEffect(power,target);
            }
            else
            {
                NewOffensiveEffect();
            }
        }

        private static void NewOffensiveItem(int power)
        {
            var stats = new List<Stat>
            {
                new Attack(power)
            };
        }

        #region Effect Generator
        private static void NewDefensiveEffect(int power, EffectTarget target)
        {
            //Filthy little effectses. THEY STOLE THE PRECIOUS
            List<Effects> effectses = new List<Effects>
            {
                new Healing(power,target,null)
            };
        }
        private static void NewOffensiveEffect()
        {
          
        }
        #endregion

        private static string GenerateName(ItemType type, EffectType nature, bool hasEffect)
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
            #region Name
            switch (type)
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
            #endregion
            #region Suffix
            switch (nature)
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
            #endregion
            int prefixIndex = _rnd.Next(prefix.Count);
            int nameIndex = _rnd.Next(name.Count);
            int suffixIndex = _rnd.Next(suffix.Count);
            var sb = new StringBuilder();

            sb.AppendFormat("{0} {1} {2}", prefix[prefixIndex],
                name[nameIndex], suffix[suffixIndex]);
            return sb.ToString();
        }
    }
}
