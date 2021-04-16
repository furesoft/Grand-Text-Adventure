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

            GameEngine.Instance.Start();
        }
    }
}
