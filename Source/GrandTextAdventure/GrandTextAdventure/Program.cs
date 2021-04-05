using System;
using GrandTextAdventure.Core;

namespace GrandTextAdventure
{
    internal class Program
    {
        private static void Main()
        {
            EntitiyPreloader.PreLoad(Environment.CurrentDirectory);

            GameEngine.Instance.Start();
        }
    }
}
