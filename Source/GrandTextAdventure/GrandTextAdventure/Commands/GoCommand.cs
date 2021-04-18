﻿using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Entities;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Commands
{
    [CommandHandler(VerbCodes.Go)]
    public class GoCommand : ICommandHandler
    {
        public void Invoke(Command cmd)
        {
            var direction = Enum.Parse<Direction>(cmd.Noun, true);

            var gameState = GameEngine.Instance.GetState();
            var pos = gameState.Player.Position;
            var newPos = Position.ApplyDirection(pos, direction);

            var currentRoom = gameState.CurrentMap;

            if (currentRoom.IsInBounds(newPos))
            {
                gameState.Player.Position = newPos;
            }
            else
            {
                GameEngine.Instance.Navigate(direction);
            }
        }
    }
}
