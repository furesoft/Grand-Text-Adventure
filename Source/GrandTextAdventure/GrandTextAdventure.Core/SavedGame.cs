using System.Linq;
using GrandTextAdventure.Core.Entities;
using LiteDB;

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

            using LiteDatabase db = new("gta.conf");
            var sg = db.GetCollection<SavedGame>("SavedGame");

            var item = sg.FindAll().First();

            var state = GameEngine.Instance.GetState();
            state.Player = item.Player;

        }
    }
}