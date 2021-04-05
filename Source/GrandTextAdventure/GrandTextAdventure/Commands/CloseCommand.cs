using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("exit")]
    public class CloseCommand : ICommand
    {
        public void Invoke(Match match)
        {
            GameEngine.Instance.Post(new EndGameMessage());
        }
    }
}
