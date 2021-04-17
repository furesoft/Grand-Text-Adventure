using System;
using System.Threading.Tasks;
using Actress;
using GrandTextAdventure.Commands;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure
{
    public class GameEngine
    {
        public static readonly GameEngine Instance = new();
        private readonly GameState _state = new();

        private MailboxProcessor<GameMessage> _mailbox;

        public object GetState(string path)
        {
            var msg = (GetStateMessage)_mailbox.PostAndReply<GameMessage>((channel) => new GetStateMessage(channel, path));

            return msg.Value;
        }

        public void Navigate(Direction direction)
        {
            _mailbox.Post(new ChangeRoomMessage { Direction = direction });
        }

        public void Post(GameMessage msg)
        {
            _mailbox.Post(msg);
        }

        public void SetState(string path, object value)
        {
            _mailbox.Post(new ChangeStateMessage { Path = path, Value = value });
        }

        public void Start()
        {
            _mailbox = MailboxProcessor.Start<GameMessage>(CommandProcessor);

            //Core.CommandProcessing.CommandProcessor.ScanForCommands(typeof(WhoAmICommand).Assembly);
            CommandHandler.Collect();

            ReadLine.AutoCompletionHandler = new AutoCompletionHandler();

            while (true)
            {
                var input = ReadLine.Read("> ");

                ReadLine.AddHistory(input);

                CommandHandler.Invoke(input);

                //Core.CommandProcessing.CommandProcessor.Invoke(input);
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
                        SetState_Internal(csMsg.Path, csMsg.Value);
                        break;

                    case GetStateMessage gsMsg:
                        {
                            var value = GetState_Internal(gsMsg.Path);

                            gsMsg.Value = value;

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

        private GameObject GetObjectBySegments(string[] segments)
        {
            if (segments.Length == 2)
            {
                switch (segments[0])
                {
                    case "player":
                        return _state.Player;

                    case "CurrentMap":
                        return _state.CurrentMap;
                }
            }
            else if (segments.Length == 1)
            {
                if (segments[0] == "CurrentMap")
                {
                    return _state.CurrentMap;
                }
            }

            return GetObjectBySegments(segments[1..^1]).GetValue<GameObject>(segments[0]);
        }

        private object GetState_Internal(string path)
        {
            var segments = ParsePath(path);

            var obj = GetObjectBySegments(segments);

            if (segments.Length > 1)
            {
                return obj.GetValue<object>(segments[^1]);
            }
            else
            {
                return obj;
            }
        }

        private static string[] ParsePath(string path)
        {
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        }

        private void SetState_Internal(string path, object value)
        {
            var segments = ParsePath(path);
            var obj = GetObjectBySegments(segments);

            if (segments.Length > 1)
            {
                obj.SetOrAddValue(segments[^1], value);
            }
            else
            {
                obj.SetOrAddValue(segments[0], value);
            }
        }
    }
}
