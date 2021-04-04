using System;
using System.Threading.Tasks;
using Actress;
using GrandTextAdventure.Messages;

namespace GrandTextAdventure
{
    internal class Program
    {
        private static MailboxProcessor<GameMessage> s_mailbox;

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
            s_mailbox = MailboxProcessor.Start<GameMessage>(CommandProcessor);

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                s_mailbox.Post(new SaveMessage());
            }
        }
    }
}
