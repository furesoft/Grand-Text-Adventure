using System;
using System.Threading.Tasks;
using Actress;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.Messages;
using GrandTextAdventure.Core.TextProcessing;

namespace GrandTextAdventure
{
    public class GameEngine
    {
        public static readonly GameEngine Instance = new();
        private GameState _state = new();

        private MailboxProcessor<GameMessage> _mailbox;

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

        public void Start()
        {
            _mailbox = MailboxProcessor.Start<GameMessage>(CommandProcessor);

            CommandHandler.Collect();

            ReadLine.AutoCompletionHandler = new AutoCompletionHandler();

            _state.CurrentMap.Init();

            while (true)
            {
                var input = ReadLine.Read("> ");

                ReadLine.AddHistory(input);

                CommandHandler.Invoke(input);
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
