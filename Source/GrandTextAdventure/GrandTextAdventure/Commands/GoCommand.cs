using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;
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

            GameEngine.Instance.Navigate(direction);
        }
    }
}
