using GrandTextAdventure.Core.Messages;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands;

[CommandHandler(VerbCodes.Quit)]
public class CloseCommand : ICommandHandler
{
    public void Invoke(Command cmd)
    {
        GameEngine.Instance.Post(new EndGameMessage());
    }
}
