using System;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core
{
    public class Tutorial
    {
        public static TutorialLine[] TutorialLines { get; set; } = new[] { new TutorialLine("Lets go to north by typing 'north' or 'go north'", new Command { Verb = VerbCodes.Go, Noun = "north" }, "Thank you") };

        public static void Start()
        {
            while (Settings.Instance.TutorialIndex < TutorialLines.Length)
            {
                var line = TutorialLines[Settings.Instance.TutorialIndex];
                Console.WriteLine(line.Description);
                var input = Console.ReadLine();
                var p = new TextProcessing.Parser();
                var cmd = p.ParseCommand(input);

                if (cmd.Equals(line.ExpectedCommand)) // ToDo need to fix
                {
                    Console.WriteLine(line.Output);
                    Settings.Instance.TutorialIndex++;
                }
                else
                {
                    Console.WriteLine("That was wrong, give it a new try");
                }

            }
        }

        public static bool IsTutorialStarted()
        {
            return Settings.Instance.WantPlayTutorial;
        }

        public static bool IsTutorialDone()
        {
            return Settings.Instance.TutorialDone || Settings.Instance.TutorialIndex < TutorialLines.Length;
        }

    }
}
