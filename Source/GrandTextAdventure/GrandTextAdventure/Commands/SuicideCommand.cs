using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands;

[CommandHandler(VerbCodes.Suicide)]
public class SuicideCommand : ICommandHandler
{
    public void Invoke(Command cmd)
    {
        var state = GameEngine.Instance.GetState();

        state.Player.Health = 0;

        GameEngine.Instance.SetState(state);

        System.Console.WriteLine("You commited suicide");
    }
}
