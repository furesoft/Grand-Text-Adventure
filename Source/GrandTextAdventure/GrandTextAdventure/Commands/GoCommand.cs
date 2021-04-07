using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.CommandProcessing;
using GrandTextAdventure.Core.Entities;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("go (north|west|east|south)")]
    [CommandPattern("go (left|right|before|behind)")]
    public class GoCommand : ICommand
    {
        public void Invoke(Match match)
        {
            var directionGroup = match.Groups[1];
            var direction = Enum.Parse<Direction>(directionGroup.Value, true);

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
