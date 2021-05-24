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
            var text = new WenceyWang.FIGlet.AsciiArt("Grand Text Adventure", font: font, width: WenceyWang.FIGlet.CharacterWidth.Smush);
            Console.WriteLine(text.ToString());
            Console.WriteLine();

            Console.WriteLine("Grand Text Adventure is an omage to the classic Grand Theft Auto but as Text Adventure.");
            Console.WriteLine("GTA can be extendet by script files for custom entities and custom maps");
            Console.WriteLine();

            Console.WriteLine("* Credits *");
            Console.WriteLine("Chris Anders (Furesoft) -> Developer");
            Console.WriteLine("Cortex -> Idea");
            Console.WriteLine();
        }
    }
}
