﻿using System;
using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure.Commands
{
    [CommandPattern("exit")]
    public class CloseCommand : ICommand
    {
        public void Invoke(Match match)
        {
            Program.Mailbox.Post(new EndGameMessage());
        }
    }
}
