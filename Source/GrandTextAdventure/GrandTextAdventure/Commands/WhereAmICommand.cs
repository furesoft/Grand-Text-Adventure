using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Location)]
    public class WhereAmICommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var value = GameEngine.Instance.GetState().CurrentMap;

            System.Console.WriteLine("You are at " + value.Name);
        }
    }
}
