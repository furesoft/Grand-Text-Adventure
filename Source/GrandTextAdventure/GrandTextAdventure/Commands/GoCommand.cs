using System;
using System.Reflection.Metadata;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Go)]
    public class GoCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var direction = Enum.Parse<Direction>(cmd.Noun, true);

            var gameState = GameEngine.Instance.GetState();
            var pos = gameState.Player.Position;
            byte speed = 1;

            if (gameState.Player.Vehicle != null)
            {
                speed = gameState.Player.Vehicle.Speed;
            }

            var newPos = Position.ApplyDirection(pos, direction, speed);

            var currentRoom = gameState.CurrentMap;

            if (currentRoom.IsInBounds(newPos))
            {
                var newObj = gameState.ObjectLayer[newPos.X, newPos.Y];

                if (newObj is IBlockable blockObj)
                {
                    if (blockObj.IsBlocked)
                    {
                        Console.WriteLine("You cannot go there. " + newObj.Name + " is blocking you"); //ToDo add new entity type for Blockable

                        return;
                    }
                }

                gameState.Player.Position = newPos;
            }
            else
            {
                GameEngine.Instance.Navigate(direction);
            }
        }
    }
}
