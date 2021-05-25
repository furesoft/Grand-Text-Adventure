using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    //ToDo implement leave command
    [CommandHandler(VerbCodes.Leave)]
    public class LeaveCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
