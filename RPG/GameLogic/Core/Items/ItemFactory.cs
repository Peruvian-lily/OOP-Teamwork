

using System.Text;
using RPG.GameLogic.Models.Effects.Base;

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
                GenerateEffect();
            }
            return null;
        }

        private static void GenerateEffect()
        {
            if (_rnd.Next(1) == 0)
            {
                NewDefensiveEffect();
            }
            else
            {
                NewOffensiveEffect();
            }
        }

        private static void NewOffensiveItem()
        {
            var stats = new List<Stat>();
        }

        private static void NewDefensiveEffect()
        {
          
        }

        private static void NewOffensiveEffect()
        {
          
        }

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
