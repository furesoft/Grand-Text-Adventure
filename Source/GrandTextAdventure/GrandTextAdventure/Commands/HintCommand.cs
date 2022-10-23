using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands;

[CommandHandler(VerbCodes.Hint)]
public class HintCommand : ICommandHandler
{
    public void Invoke(Command cmd)
    {
        System.Console.WriteLine("There is no Help at the moment available");
    }
}