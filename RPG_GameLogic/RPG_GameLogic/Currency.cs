using System;
using RPG_GameLogic.Interfaces;

namespace RPG_GameLogic
{
    public class Currency : IComparable
    {
        // Fields :
        private int gold;
        private int silver;
        private int cooper;

        private const int MAX_COOPER = 100;
        private const int MAX_SILVER = 100;

        // Constructor :
        public Currency(int gold, int silver, int cooper)
        {
            this.Gold = gold;
            this.Silver = silver;
            this.Cooper = cooper;
        }
        public Currency(Currency curr)
        {
            this.Gold = curr.Gold;
            this.Silver = curr.Silver;
            this.Cooper = curr.Cooper;
        }
        public Currency() : this(0, 0, 0) { }

        // Properties :
        public int Gold
        {
            get { return this.gold; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Gold can't be negative.");
                }

                this.gold = value;
            }
        }

        public int Silver
        {
            get { return this.silver; }
            private set
            {
                if (value < 0 || value >= 100)
                {
                    throw new ArgumentException("Silver Must be in [0..100].");
                }

                this.silver = value;
            }
        }

        public int Cooper
        {
            get { return this.cooper; }
            private set
            {
                if (value < 0 || value >= 100)
                {
                    throw new ArgumentException("Cooper must be in [0..100].");
                }

                this.cooper = value;
            }
        }

        //Methods :
        public override bool Equals(object obj)
        {
            if (obj is Currency)
            {
                Currency otherCurrency = (Currency)obj;

                if (this.Cooper == otherCurrency.Cooper &&
                    this.Silver == otherCurrency.Silver &&
                    this.Gold == otherCurrency.Gold)
                {
                    return true;
                }
            }

            return false;
        }

        public Currency Add(Currency curr)
        {
            int cooperSum = this.Cooper + curr.Cooper;
            int silverSum = this.Silver + curr.Silver;
            int goldSum = this.Gold + curr.Gold;

            if (cooperSum >= MAX_COOPER)
            {
                cooperSum = cooperSum - MAX_COOPER;
                silverSum++;
            }

            if (silverSum >= MAX_SILVER)
            {
                silverSum = silverSum - MAX_SILVER;
                goldSum++;
            }

            return new Currency(goldSum, silverSum, cooperSum);
        }

        public Currency Subtract(Currency curr)
        {
            int cooperDiff = this.Cooper - curr.Cooper;
            int silverDiff = this.Silver - curr.Silver;
            int goldDiff = this.Gold - curr.Gold;

            if (goldDiff < 0)
            {
                throw new ArgumentException("Currency becomes negative after subtraction!");
            }

            if (silverDiff < 0)
            {
                if (goldDiff <= 0)
                {
                    throw new ArgumentException("Currency becomes negative after subtraction!");
                }
                else
                {
                    goldDiff--;
                    silverDiff = MAX_SILVER - Math.Abs(silverDiff);
                }
            }

            if (cooperDiff < 0)
            {
                if (silverDiff <= 0)
                {
                    if (goldDiff <= 0)
                    {
                        throw new ArgumentException("Currency becomes negative after subtraction!");
                    }
                    else
                    {
                        goldDiff--;
                        silverDiff = MAX_SILVER - 1;
                        cooperDiff = MAX_COOPER - Math.Abs(cooperDiff);
                    }
                }
                else
                {
                    silverDiff--;
                    cooperDiff = MAX_COOPER - Math.Abs(cooperDiff);
                }
            }

            return new Currency(goldDiff, silverDiff, cooperDiff);
        }

        public int CompareTo(object obj)
        {
            if (obj is Currency)
            {
                Currency other = (Currency)obj;

                if (this.Gold > other.Gold)
                {
                    return 1;
                }
                else if (this.Gold == other.Gold)
                {
                    if (this.Silver > other.Silver)
                    {
                        return 1;
                    }
                    else if (this.Silver == other.Silver)
                    {
                        if (this.Cooper > other.Cooper)
                        {
                            return 1;
                        }
                        else if (this.Cooper == other.Cooper)
                        {
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                throw new ArgumentException("Only Currency objects can be compared to other Currency objects.");
            }
        }

        public static Currency operator +(Currency lhs, Currency rhs)
        {
            return lhs.Add(rhs);
        }
        public static Currency operator -(Currency lhs, Currency rhs)
        {
            return lhs.Subtract(rhs);
        }

        public static bool operator ==(Currency lhs, Currency rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Currency lhs, Currency rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator >(Currency lhs, Currency rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator <(Currency lhs, Currency rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator >=(Currency lhs, Currency rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        public static bool operator <=(Currency lhs, Currency rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("G = {0}, S = {1}, C = {2}", this.Gold, this.Silver, this.Cooper);
        }
    }
}
