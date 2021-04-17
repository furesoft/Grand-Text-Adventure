using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Look)]
    public class LookCommand : ICommandHandler
    {
        public void Invoke(Match match)
        {
            var directionGroup = match.Groups[1];
            var direction = Enum.Parse<Direction>(directionGroup.Value, true);
        }

        public void Invoke(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
