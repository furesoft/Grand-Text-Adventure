using System;
using System.Collections.Generic;
using System.Linq;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Look)]
    public class LookCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var direction = Enum.Parse<Direction>(cmd.Noun, true);
            var gameState = GameEngine.Instance.GetState();
            var pos = gameState.Player.Position;

            if (direction == Direction.Around)
            {
                var aroundObjects = GetAroundObjects(gameState, pos).Where(_ => _.Item2 != null);

                if (aroundObjects.Any())
                {
                    foreach (var item in aroundObjects)
                    {
                        Console.WriteLine(item.Item1 + ": " + item.Item2.Name);
                    }
                }
                else
                {
                    Console.WriteLine("There is nothing");
                }
            }
            else
            {
                var newPos = Position.ApplyDirection(pos, direction);

                if (gameState.CurrentMap.IsInBounds(newPos))
                {

                    var obj = GetObject(gameState, newPos);

                    if (obj != null)
                    {
                        Console.WriteLine("It is a {0}", obj.Name ?? obj.GetType().Name);
                    }
                    else
                    {
                        Console.WriteLine("There is nothing");
                    }
                }
                else
                {
                    Console.WriteLine("There is nothing");
                }
            }
        }

        private static IEnumerable<(Direction, GameObject)> GetAroundObjects(GameState gameState, Position pos)
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

        private static GameObject GetObject(GameState gameState, Position newPos)
        {
            if (newPos.Y < 0 || newPos.Y >= gameState.ObjectLayer.GetLength(0) || newPos.X < 0 || newPos.X >= gameState.ObjectLayer.GetLength(1))
            {
                throw new IndexOutOfRangeException("Out of Bounds: " + newPos);

            }

            return gameState.ObjectLayer[newPos.X, newPos.Y];
        }
    }
}
