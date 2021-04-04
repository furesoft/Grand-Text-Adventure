using System;
using System.Threading.Tasks;
using Actress;
using GrandTextAdventure.Commands;
using GrandTextAdventure.Core.Game;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure
{
    internal class Program
    {
        public static MailboxProcessor<GameMessage> Mailbox;
        private static readonly GameState s_state = new();

        private static async Task CommandProcessor(MailboxProcessor<GameMessage> inbox)
        {
            while (true)
            {
                var msg = await inbox.Receive();

                if (msg is EndGameMessage)
                {
                    Console.WriteLine("The Game will exit soon...");
                    await Task.Delay(2000);

                    Environment.Exit(0);
                }
                else if (msg is LoadMessage ldMsg)
                {
                    Console.WriteLine("Game loaded");
                }
                else if (msg is SaveMessage savMsg)
                {
                    Console.WriteLine("Game saved");
                }
                else if (msg is ChangeStateMessage csMsg)
                {
                }
                else if (msg is GetStateMessage gsMsg)
                {
                    var value = GetState(gsMsg.Path);

                    gsMsg.Value = value;

                    gsMsg.Channel.Reply(gsMsg);
                }
            }
        }

        private static object GetState(string path)
        {
            var segments = ParsePath(path);

            if (segments[0] == "player")
            {
                return s_state.Player.GetValue<object>(segments[1]);
            }

            return null;
        }

        private static void Main()
        {
            Mailbox = MailboxProcessor.Start<GameMessage>(CommandProcessor);

            Core.CommandProcessing.CommandProcessor.ScanForCommands(typeof(WhoAmICommand).Assembly);

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                Core.CommandProcessing.CommandProcessor.Invoke(input);
            }
        }

        private static string[] ParsePath(string path)
        {
            return path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
