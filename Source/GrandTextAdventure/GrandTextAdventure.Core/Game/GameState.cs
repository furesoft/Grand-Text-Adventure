﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core.Game
{
    public class GameState
    {
        public GameState()
        {
            Player.Name = "Michael";
            Player.SetOrAddValue("Money", new Money(500, 1));

            var outside = new Room { Name = "Baker Street", Width = 10, Heigth = 10 };
            outside.Exits.NorthID = new RoomID { ID = "Suffer Street" };
            outside.Exits.SouthID = new RoomID { ID = "London Street", };
            outside.Exits.EastID = new RoomID { ID = "Hospital Street" };
            outside.Exits.WestID = new RoomID { ID = "Ball Street" };

            Player.Position = new(0, 0);
            Player.OnDead += (v) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wasted");
                Console.ResetColor();
            };

            outside.PlacingItems.Add(new Position(0, 0), new Vehicle() { Name = "Lambo", IsLocked = true });
            outside.PlacingItems.Add(new Position(0, 1), new NPC() { Name = "Man", Vehicle = new Vehicle { Name = "Bycicle" } });
            outside.PlacingItems.Add(new Position(1, 2), new Vehicle { Name = "Fiat" });
            outside.PlacingItems.Add(new Position(1, 1), new NPC() { Name = "Woman" });
            outside.PlacingItems.Add(new Position(2, 1), new BlockingEntity() { Name = "Wall" });

            RoomManager.AddRoom(new RoomID("Baker Street"), outside);

            CurrentMap = new Building
            {
                Name = "Michael's Home",
                Exits = new RoomExits() { NorthID = new RoomID("Baker Street") }
            };
            CurrentMap.Width = 10;
            CurrentMap.Heigth = 5;
        }


        //ToDo Refeactor GetAroundObjects
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

            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.NorthEast)))
            {
                yield return (Direction.NorthEast, GetObject(gameState, Position.ApplyDirection(pos, Direction.NorthEast)));
            }
            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.NorthWest)))
            {
                yield return (Direction.NorthWest, GetObject(gameState, Position.ApplyDirection(pos, Direction.NorthWest)));
            }
            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.SouthEast)))
            {
                yield return (Direction.SouthEast, GetObject(gameState, Position.ApplyDirection(pos, Direction.SouthEast)));
            }
            if (gameState.CurrentMap.IsInBounds(Position.ApplyDirection(pos, Direction.SouthWest)))
            {
                yield return (Direction.SouthWest, GetObject(gameState, Position.ApplyDirection(pos, Direction.SouthWest)));
            }
        }

        public static GameObject GetObject(GameState gameState, Position newPos)
        {
            if (newPos.Y < 0 || newPos.Y >= gameState.ObjectLayer.GetLength(0) || newPos.X < 0 || newPos.X >= gameState.ObjectLayer.GetLength(1))
            {
                throw new IndexOutOfRangeException("Out of Bounds: " + newPos);

            }

            var obj = gameState.ObjectLayer[newPos.X, newPos.Y];
            if (obj is NPC npc && npc.Vehicle is not null)
            {
                return npc.Vehicle;
            }

            return obj;
        }

        public Room CurrentMap { get; set; }
        public PlayerCharackter Player { get; set; } = new();

        public GameObject[,] ObjectLayer { get; set; } // used for Vehicles/NPCs/Items
    }
}
