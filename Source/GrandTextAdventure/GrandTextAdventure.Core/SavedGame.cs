using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core
{
    public class SavedGame
    {
        public RoomID CurrentRoom { get; set; }
        public PlayerCharackter Player { get; set; }

        public void Load()
        {
            // ToDo load SavedGame from LiteDB Entity
            // Apply to GameState
        }
    }
}