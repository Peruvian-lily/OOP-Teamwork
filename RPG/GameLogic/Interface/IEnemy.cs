namespace RPG.GameLogic.Interface
{
    using System.Collections.Generic;
    using RPG.GameLogic.Core.Factory;
    using RPG.GameLogic.Models.Characters.Base;
    using RPG.GameLogic.Models.Stats.Base;

    public interface IEnemy : IFight
    {
        List<Stat> Stats { get; }

        EnemyType EnemyType { get; set; }

        List<Character> GetAllies(List<Character> allGameCharacters);
    }
}