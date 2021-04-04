using System;
using System.Threading.Tasks;
using Actress;
using GrandTextAdventure.Commands;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure
{
    internal class Program
    {
        public static MailboxProcessor<GameMessage> Mailbox;

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
            }
        }

        private static void Main()
        {
            Mailbox = MailboxProcessor.Start<GameMessage>(CommandProcessor);

            Core.CommandProcessing.CommandProcessor.Register<LookCommand>();
            Core.CommandProcessing.CommandProcessor.Register<CloseCommand>();

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                Core.CommandProcessing.CommandProcessor.Invoke(input);
            }
        }
    }
}
