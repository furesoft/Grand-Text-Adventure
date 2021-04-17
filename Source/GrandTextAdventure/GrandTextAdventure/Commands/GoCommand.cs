using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Entities;
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

            var pos = (Position)GameEngine.Instance.GetState("/player/Position");
            var newPos = Position.ApplyDirection(pos, direction);

            var currentRoom = (Room)GameEngine.Instance.GetState("/CurrentMap");

            if (currentRoom.IsInBounds(newPos))
            {
                GameEngine.Instance.SetState("/player/Position", newPos);
            }
            else
            {
                GameEngine.Instance.Navigate(direction);
            }
        }
    }
}
