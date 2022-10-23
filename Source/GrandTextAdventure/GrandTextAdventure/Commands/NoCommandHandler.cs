using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands;

[CommandHandler(VerbCodes.NoCommand)]
public class NoCommandHandler : ICommandHandler
{
    public void Invoke(Command cmd)
    {
        System.Console.WriteLine("I can't understand what you mean");
    }
}
