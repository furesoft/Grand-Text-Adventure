using GrandTextAdventure.Core;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands;

[CommandHandler(VerbCodes.Call)]
public class CallCommand : ICommandHandler
{
    public void Invoke(Command cmd)
    {
        Phone.Dial(cmd.Noun);
    }
}