using System.Text.RegularExpressions;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.CommandProcessing;

namespace GrandTextAdventure.Commands
{
    [CommandPattern(@"How much (money) do I have\?")]
    [CommandPattern(@"How many (dollar[s]?|coins) do I have\?")]
    public class HowManyDollarsCommand : ICommand
    {
        public void Invoke(Match args)
        {
            var money = (Money)GameEngine.Instance.GetState("/player/Money");

            switch (args.Groups[1].Value)
            {
                case "money":
                    System.Console.WriteLine("You have {0} $ and {1} Pilzschaf-Coins", money.Dollar, money.PilzschafCoins);
                    break;

                case "dollars":
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
