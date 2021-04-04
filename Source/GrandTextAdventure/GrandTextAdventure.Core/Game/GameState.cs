using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core.Game
{
    public class GameState
    {
        public Map CurrentMap { get; set; }
        public Charackter Player { get; set; } = new Charackter();
    }
}
