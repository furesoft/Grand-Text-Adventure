using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("look (north|west|east|south)")]
    [CommandPattern("look (left|right|before|behind)")]
    public class LookCommand : ICommand
    {
        public void Invoke(Match match)
        {
            var directionGroup = match.Groups[1];
            var direction = Enum.Parse<Direction>(directionGroup.Value, true);
        }
    }
}
