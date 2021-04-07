using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core.Game
{
    public class GameState
    {
        public GameState()
        {
            Player.SetOrAddValue("Money", new Money { PilzschafCoins = 1, Dollar = 500 });
            CurrentMap = new Room { Name = "Baker Street" };
            CurrentMap.Exits.North = new Room { Name = "Suffer Street", Exits = new RoomExits { South = CurrentMap } };
            CurrentMap.Exits.South = new Room { Name = "London Street", Exits = new RoomExits { North = CurrentMap } };
            CurrentMap.Exits.East = new Room { Name = "Hospital Street", Exits = new RoomExits { West = CurrentMap } };
            CurrentMap.Exits.West = new Room { Name = "Ball Street", Exits = new RoomExits { East = CurrentMap } };
        }

        public Room CurrentMap { get; set; }
        public Charackter Player { get; set; } = new Charackter();
    }
}
