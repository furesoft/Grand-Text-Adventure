using System;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;
using Actress;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Entities;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.Messages;
using GrandTextAdventure.Core.TextProcessing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GrandTextAdventure
{
    public class GameEngine
    {
        public static readonly GameEngine Instance = new();
        private GameState _state = new();

        private MailboxProcessor<GameMessage> _mailbox;

        private MailboxProcessor<GameMessage> _npcMailbox;

        public GameState GetState()
        {
            var msg = (GetStateMessage)_mailbox.PostAndReply<GameMessage>((channel) => new GetStateMessage(channel));

            return msg.State;
        }

        public void Navigate(Direction direction)
        {
            _mailbox.Post(new ChangeRoomMessage { Direction = direction });
        }

        public void Post(GameMessage msg)
        {
            _mailbox.Post(msg);
        }

        public void SetState(GameState state)
        {
            _mailbox.Post(new ChangeStateMessage { State = state });
        }

        public bool Wait(int milliseconds)
        {
            var msg = (WaitMessage)_mailbox.PostAndReply<GameMessage>((channel) => new WaitMessage(channel, milliseconds));

            return msg.IsDone;
        }

        public static void Hint(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("<Hint>");
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Start()
        {
            _mailbox = MailboxProcessor.Start<GameMessage>(CommandProcessor);
            _npcMailbox = MailboxProcessor.Start<GameMessage>(NpcProcessor);

            var npcTimer = new Timer
            {
                Interval = 5000
            };
            npcTimer.Elapsed += NpcTimer_ellapsed;
            npcTimer.Start();

            CommandHandler.Collect();

            ReadLine.AutoCompletionHandler = new AutoCompletionHandler();

            _state.CurrentMap.Init();

            //ToDo: load dialogs from files
            var startConv = new DialogItem("Simon", new string[]{
                "Hey Michael.",
                "Nice to see ya. The plan is going. How was your flight?",

            }, new("Michael", new string[]{
                "It was fine. But please let me go at home.", "<wait>", "I will sleep and tomorrow we can continue our plan."
            }, new("", new[] { "<wait>", "The next day...", "<wait>", "It is a sunny day in Los Santos.", "<wait>", "Knock Knock..", "<wait>", "Knock Knock..", "<wait>" }, new("Simon", new[] { "Michael, are you awake?", "We need to go!" }, null, null))), null);

            var rootDialog = new DialogItem("", new string[] { "Monday, 17:30 at Los Santos Airport", "<wait>", "<wait>" }, startConv, null);

            Dialog.Start(rootDialog);
            Hint("Leave your House and follow the Instructions from Simon");

            while (true)
            {
                var input = ReadLine.Read("> ");

                ReadLine.AddHistory(input);

                CommandHandler.Invoke(input);
            }
        }

        private void NpcTimer_ellapsed(object sender, ElapsedEventArgs e)
        {
            var layer = GetState().ObjectLayer;
            var npcs = GetNpcs(layer);

            if (npcs.Any())
            {
                foreach (var npc in npcs)
                {
                    _npcMailbox.Post(new MoveNpcMessage { Direction = Direction.North, OldPosition = npc.Position });
                }
            }
        }

        private static IEnumerable<NPC> GetNpcs(GameObject[,] layer)
        {
            var result = new List<NPC>();

            for (var i = 0; i < layer.GetLength(0); i++)
            {
                for (var j = 0; j < layer.GetLength(1); j++)
                {
                    var obj = layer[i, j];

                    if (obj is NPC npc)
                    {
                        result.Add(npc);
                    }
                }
            }

            return result;
        }

        private async Task NpcProcessor(MailboxProcessor<GameMessage> arg)
        {
            while (true)
            {
                var msg = await arg.Receive();
                var gameState = Instance.GetState();

                switch (msg)
                {
                    case MoveNpcMessage movMsg:

                        var npc = gameState.ObjectLayer[movMsg.OldPosition.X, movMsg.OldPosition.Y];
                        if (npc is Charackter c)
                        {
                            var newPos = Position.ApplyDirection(movMsg.OldPosition, movMsg.Direction);
                            var newNpc = gameState.ObjectLayer[newPos.X, newPos.Y];

                            if (newNpc == null)
                            {
                                c.Position = newPos;

                                gameState.ObjectLayer[newPos.X, newPos.Y] = c;
                                gameState.ObjectLayer[movMsg.OldPosition.X, movMsg.OldPosition.Y] = null;

                                SetState(gameState);
                            }
                        }

                        break;
                }
            }
        }

        private async Task CommandProcessor(MailboxProcessor<GameMessage> inbox)
        {
            while (true)
            {
                var msg = await inbox.Receive();

                switch (msg)
                {
                    case EndGameMessage:
                        Console.WriteLine("The Game will exit soon...");
                        await Task.Delay(2000);

                        Environment.Exit(0);
                        break;
                    case WaitMessage waitMsg:

                        await Task.Delay(waitMsg.WaitTime);
                        waitMsg.IsDone = true;

                        waitMsg.Channel.Reply(waitMsg);

                        break;

                    case LoadMessage:
                        Console.WriteLine("Game loaded");
                        break;

                    case SaveMessage:
                        Console.WriteLine("Game saved");
                        break;

                    case ChangeStateMessage csMsg:
                        _state = csMsg.State;
                        break;

                    case GetStateMessage gsMsg:
                        {
                            gsMsg.State = _state;

                            gsMsg.Channel.Reply(gsMsg);
                            break;
                        }
                    case ChangeRoomMessage crMsg:
                        switch (crMsg.Direction)
                        {
                            case Direction.North:
                                if (_state.CurrentMap.Exits.North != null)
                                {
                                    _state.CurrentMap = _state.CurrentMap.Exits.North;
                                }
                                else
                                {
                                    Console.WriteLine("You are at the End of the World");
                                }

                                break;

                            case Direction.West:
                                if (_state.CurrentMap.Exits.West != null)
                                {
                                    _state.CurrentMap = _state.CurrentMap.Exits.West;
                                }
                                else
                                {
                                    Console.WriteLine("You are at the End of the World");
                                }
                                break;

                            case Direction.East:
                                if (_state.CurrentMap.Exits.East != null)
                                {
                                    _state.CurrentMap = _state.CurrentMap.Exits.East;
                                }
                                else
                                {
                                    Console.WriteLine("You are at the End of the World");
                                }
                                break;

                            case Direction.South:
                                if (_state.CurrentMap.Exits.South != null)
                                {
                                    _state.CurrentMap = _state.CurrentMap.Exits.South;
                                }
                                else
                                {
                                    Console.WriteLine("You are at the End of the World");
                                }
                                break;
                        }

                        break;
                }
            }
        }
    }
}
