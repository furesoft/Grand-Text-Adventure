using System;
using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core.Game
{
    public class GameState
    {
        public GameState()
        {
            Player.Name = "Michael";
            Player.SetOrAddValue("Money", new Money { PilzschafCoins = 1, Dollar = 500 });

            CurrentMap = new Room { Name = "Baker Street", Width = 10, Heigth = 10 };
            CurrentMap.Exits.North = new Room { Name = "Suffer Street", Exits = new RoomExits { South = CurrentMap } };
            CurrentMap.Exits.South = new Room { Name = "London Street", Exits = new RoomExits { North = CurrentMap } };
            CurrentMap.Exits.East = new Room { Name = "Hospital Street", Exits = new RoomExits { West = CurrentMap } };
            CurrentMap.Exits.West = new Room { Name = "Ball Street", Exits = new RoomExits { East = CurrentMap } };

            Player.Position = new(0, 0);
            Player.OnDead += (p, v) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wasted");
                Console.ResetColor();
            };

            CurrentMap.PlacingItems.Add(new Position(0, 0), new Vehicle() { Name = "Lambo" });
            CurrentMap.PlacingItems.Add(new Position(0, 1), new Charackter() { Name = "Man" });
            CurrentMap.PlacingItems.Add(new Position(1, 2), new Vehicle { Name = "Fiat" });
            CurrentMap.PlacingItems.Add(new Position(1, 1), new Charackter() { Name = "Woman" });

        }

        public Room CurrentMap { get; set; }
        public PlayerCharackter Player { get; set; } = new();

        public GameObject[,] ObjectLayer { get; set; } // used for Vehicles/NPCs/Items
    }
}
