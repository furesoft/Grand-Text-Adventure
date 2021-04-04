using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core.Game
{
    public class GameState
    {
        public GameState()
        {
            Player.SetOrAddValue("Money", new Money { PilzschafCoins = 1, Dollar = 500 });
        }

        public Map CurrentMap { get; set; }
        public Charackter Player { get; set; } = new Charackter();
    }
}
