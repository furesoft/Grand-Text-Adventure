using System;
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
        }
    }
}
