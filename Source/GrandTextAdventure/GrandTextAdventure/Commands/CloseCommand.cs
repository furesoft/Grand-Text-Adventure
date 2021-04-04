using System;
using GrandTextAdventure.Core.CommandProcessing;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("exit")]
    public class CloseCommand : ICommand
    {
        public void Invoke(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
