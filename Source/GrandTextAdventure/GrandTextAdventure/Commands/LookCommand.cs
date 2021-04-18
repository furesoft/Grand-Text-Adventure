using System;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Look)]
    public class LookCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var direction = Enum.Parse<Direction>(cmd.Noun, true);
            var gameState = GameEngine.Instance.GetState();
            var pos = gameState.Player.Position;
            var newPos = Position.ApplyDirection(pos, direction);

            var obj = GetObject(gameState, newPos);

            System.Console.WriteLine("It is a {0}", obj.Name ?? obj.GetType().Name);
        }

        private static GameObject GetObject(GameState gameState, Position newPos)
        {
            if (newPos.Y < 0 || newPos.Y >= gameState.ObjectLayer.GetLength(0) || newPos.X < 0 || newPos.X >= gameState.ObjectLayer.GetLength(1))
            {
                throw new IndexOutOfRangeException("Out of Bounds: " + newPos);

            }

            return gameState.ObjectLayer[newPos.X, newPos.Y];
        }
    }
}
