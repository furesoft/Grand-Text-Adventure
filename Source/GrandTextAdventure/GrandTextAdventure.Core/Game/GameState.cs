using System;
using System.Collections.Generic;
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

            CurrentMap.PlacingItems.Add(new Position(0, 0), new Vehicle() { Name = "Lambo", IsLocked = true });
            CurrentMap.PlacingItems.Add(new Position(0, 1), new Charackter() { Name = "Man" });
            CurrentMap.PlacingItems.Add(new Position(1, 2), new Vehicle { Name = "Fiat" });
            CurrentMap.PlacingItems.Add(new Position(1, 1), new Charackter() { Name = "Woman" });

        }

        public static IEnumerable<(Direction Direction, GameObject GameObject)> GetAroundObjects(GameState gameState, Position pos)
        {
            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.North)))
            {
                yield return (Direction.North, GetObject(gameState, Position.ApplyDirection(pos, Direction.North)));
            }
            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.South)))
            {
                yield return (Direction.South, GetObject(gameState, Position.ApplyDirection(pos, Direction.South)));
            }
            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.West)))
            {
                yield return (Direction.West, GetObject(gameState, Position.ApplyDirection(pos, Direction.West)));
            }
            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.East)))
            {
                yield return (Direction.East, GetObject(gameState, Position.ApplyDirection(pos, Direction.East)));
            }

            //ToDo: implement GetAroundObjects for combined directions
        }

        public static GameObject GetObject(GameState gameState, Position newPos)
        {
            if (newPos.Y < 0 || newPos.Y >= gameState.ObjectLayer.GetLength(0) || newPos.X < 0 || newPos.X >= gameState.ObjectLayer.GetLength(1))
            {
                throw new IndexOutOfRangeException("Out of Bounds: " + newPos);

            }

            return gameState.ObjectLayer[newPos.X, newPos.Y];
        }

        public Room CurrentMap { get; set; }
        public PlayerCharackter Player { get; set; } = new();

        public GameObject[,] ObjectLayer { get; set; } // used for Vehicles/NPCs/Items
    }
}
