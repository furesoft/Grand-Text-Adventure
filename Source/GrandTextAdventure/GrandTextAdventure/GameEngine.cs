using System;
using System.Threading;
using System.Threading.Tasks;
using Actress;
using GrandTextAdventure.Commands;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Game;
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

            Core.CommandProcessing.CommandProcessor.ScanForCommands(typeof(WhoAmICommand).Assembly);

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                Core.CommandProcessing.CommandProcessor.Invoke(input);
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

                    case LoadMessage ldMsg:
                        Console.WriteLine("Game loaded");
                        break;

                    case SaveMessage savMsg:
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
                }
            }
        }

        private GameObject GetObjectBySegments(string[] segments)
        {
            if (segments.Length == 2)
            {
                if (segments[0] == "player")
                {
                    return _state.Player;
                }
            }

            return GetObjectBySegments(segments[1..^1]).GetValue<GameObject>(segments[0]);
        }

        private object GetState_Internal(string path)
        {
            var segments = ParsePath(path);

            var obj = GetObjectBySegments(segments);

            return obj.GetValue<object>(segments[^1]);
        }

        private string[] ParsePath(string path)
        {
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        }

        private void SetState_Internal(string path, object value)
        {
            var segments = ParsePath(path);
            var obj = GetObjectBySegments(segments);

            obj.SetOrAddValue(segments[^1], value);
        }
    }
}
