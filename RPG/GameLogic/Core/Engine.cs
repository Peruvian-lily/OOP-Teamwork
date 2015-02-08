namespace RPG.GameLogic.Core
{
    public class Engine
    {
        private static Engine _instance;

        public static Engine GetInstance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new Engine();
                }

                return _instance;
            }
        }

        private Engine() 
        {
            
        }
    }
}
