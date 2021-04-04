using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("look (south|west|east|south)")]
    [CommandPattern("look (left|right|before|behind)")]
    public class LookCommand : ICommand
    {
        public void Invoke(Match match)
        {
            var direction = match.Groups[1];
        }
    }
}
