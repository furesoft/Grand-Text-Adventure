using GrandTextAdventure.Core;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    //ToDo implement leave command
    [CommandHandler(VerbCodes.Leave)]
    public class LeaveCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var gameState = GameEngine.Instance.GetState();
            gameState.CurrentMap = RoomManager.GetRoom(gameState.CurrentMap.Exits.NorthID);

            if (gameState.CurrentMap is IEnterable enterable)
            {
                enterable.OnExit(gameState.Player.Position);
            }

            GameEngine.Instance.SetState(gameState);
        }
    }
}
