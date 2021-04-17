using System.Text.RegularExpressions;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Money)]
    public class HowManyDollarsCommand : ICommandHandler
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

        public void Invoke(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
