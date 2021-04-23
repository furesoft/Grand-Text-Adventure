using System;
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
            var gameState = GameEngine.Instance.GetState();
            var room = gameState.CurrentMap;

            if (gameState.Player.Vehicle != null)
            {
                Console.WriteLine("You are driving with " + gameState.Player.Vehicle.Name + " in " + room.Name);
            }
            else
            {
                Console.WriteLine("You are at " + room.Name);
            }
        }
    }
}
