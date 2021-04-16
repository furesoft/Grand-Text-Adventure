using System;
using System.Reflection;

namespace GrandTextAdventure
{
    public static class ConsoleBanner
    {
        public static void Display()
        {
            // Use FigLet font for Banner
            var fontStream = Assembly.GetEntryAssembly().GetManifestResourceStream("GrandTextAdventure.font.flf");
            var font = new WenceyWang.FIGlet.FIGletFont(fontStream);
            var text = new WenceyWang.FIGlet.AsciiArt("Grand Text Adventure", font: font, width: WenceyWang.FIGlet.CharacterWidth.Full);
            Console.WriteLine(text.ToString());
            Console.WriteLine();
        }
    }
}
