using System.Text.RegularExpressions;
using GrandTextAdventure.Core.Entities;
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
            var value = (Room)GameEngine.Instance.GetState("/CurrentMap");

            System.Console.WriteLine("You are at " + value.Name);
        }
    }
}
