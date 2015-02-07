namespace Game.Models.Stats
{
    abstract class Stats
    {
        private string _name;
        private int _value;

        protected Stats(string name,int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }

        public int Value
        {
            get;
            private set
            {
                
            }
        }
    }
}
