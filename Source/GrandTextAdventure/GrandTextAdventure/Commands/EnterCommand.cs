using System.Linq;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{

    [CommandHandler(VerbCodes.Enter)]
    public class EnterCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var gameState = GameEngine.Instance.GetState();
            var pos = gameState.Player.Position;

            var aroundObjects = GameState.GetAroundObjects(gameState, pos).Where(_ => _.GameObject != null);

            if (aroundObjects.Any())
            {
                if (string.IsNullOrEmpty(cmd.Noun))
                {
                    // search for any enterable object and enter it
                    foreach (var obj in aroundObjects)
                    {
                        if (obj.GameObject is IEnterable enterableObj)
                        {
                            if (enterableObj.IsEnterable())
                            {
                                var newPos = Position.ApplyDirection(pos, obj.Direction);

                                enterableObj.OnEnter(newPos);
                            }
                        }
                    }
                }
                else
                {
                    // check for the given noun, if its enterable enter it
                    foreach (var obj in aroundObjects)
                    {

                        if (obj.GameObject.Name.ToLower() == cmd.Noun)
                        {
                            if (obj.GameObject is IEnterable enterableObj)
                            {
                                var newPos = Position.ApplyDirection(pos, obj.Direction);

                                enterableObj.OnEnter(newPos);
                            }
                        }
                    }
                }
            }
            else
            {
                System.Console.WriteLine("There is nothing");
            }
        }
    }
}
