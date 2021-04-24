using System;
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
                var aroundObjects = GameState.GetAroundObjects(gameState, pos).Where(_ => _.GameObject != null);

                if (aroundObjects.Any())
                {
                    foreach (var item in aroundObjects)
                    {
                        Console.WriteLine(item.Direction + ": " + item.GameObject.Name);
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

                    var obj = GameState.GetObject(gameState, newPos);

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
    }
}
