using System.Text.RegularExpressions;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.CommandProcessing;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("How much (money|dollar|coins) do I have?")]
    public class HowManyDollarsCommand : ICommand
    {
        public void Invoke(Match args)
        {
            var msg = (GetStateMessage)Program.Mailbox.PostAndReply<GameMessage>((channel) => new GetStateMessage(channel, "/player/Money"));
            var money = (Money)msg.Value;

            switch (args.Groups[1].Value)
            {
                case "money":
                    System.Console.WriteLine("You have {0} $ and {1} Pilzschaf-Coins", money.Dollar, money.PilzschafCoins);
                    break;

                case "dollar":
                    System.Console.WriteLine("You have {0} $", money.Dollar);
                    break;

                case "coins":
                    System.Console.WriteLine("You have {0} Pilzschaf-Coins", money.PilzschafCoins);
                    break;
            }
        }
    }
}
