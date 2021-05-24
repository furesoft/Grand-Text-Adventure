using GrandTextAdventure.Core;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Money)]
    public class HowManyDollarsCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var money = GameEngine.Instance.GetState().Player.Money;

            System.Console.WriteLine("You have {0} $ and {1} Pilzschaf-Coins", money.Dollar, money.PilzschafCoins);
        }
    }
}
