using System;
using System.Reflection;
using GrandTextAdventure.Core;

namespace GrandTextAdventure
{
    internal class Program
    {
        private static void Main()
        {
            var fontStream = Assembly.GetEntryAssembly().GetManifestResourceStream("GrandTextAdventure.font.flf");
            var font = new WenceyWang.FIGlet.FIGletFont(fontStream);
            var text = new WenceyWang.FIGlet.AsciiArt("Grand Text Adventure", font: font, width: WenceyWang.FIGlet.CharacterWidth.Full);
            Console.WriteLine(text.ToString());

            EntitiyPreloader.PreLoad(Environment.CurrentDirectory);

            GameEngine.Instance.Start();
        }
    }
}
