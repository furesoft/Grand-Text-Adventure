using System;
using GrandTextAdventure.Core;

namespace GrandTextAdventure
{
    internal class Program
    {
        private static void Main()
        {
            ConsoleBanner.Display();

            EntitiyPreloader.PreLoad(Environment.CurrentDirectory);

            if (Settings.Instance.IsFirstStart)
            {
                Console.Write("Do you want to play an tutorial to learn how to use this game? (y|n)");
                var wantTut = Console.ReadKey().Key;

                Settings.Instance.WantPlayTutorial = wantTut == ConsoleKey.Y;
                Settings.Instance.IsFirstStart = false;
            }

            if (Tutorial.IsTutorialStarted() || Settings.Instance.WantPlayTutorial)
            {
                Tutorial.Start();
            }
            else
            {
                GameEngine.Instance.Start();
            }
        }
    }
}
