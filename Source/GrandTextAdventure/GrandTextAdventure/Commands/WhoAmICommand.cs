using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("Who am I?")]
    public class WhoAmICommand : ICommand
    {
        public void Invoke(Match args)
        {
            var msg = (GetStateMessage)Program.Mailbox.PostAndReply<GameMessage>((channel) => new GetStateMessage(channel, "/player/Name"));

            System.Console.WriteLine("You are " + msg.Value);
        }
    }
}
