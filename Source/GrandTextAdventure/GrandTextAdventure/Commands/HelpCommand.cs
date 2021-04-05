using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;

namespace GrandTextAdventure.Commands
{
    [CommandPattern(@"What can I say\?")]
    internal class HelpCommand : ICommand
    {
        public void Invoke(Match args)
        {
            CommandProcessor.DisplayCommands();
        }
    }
}
