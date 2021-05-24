using System;
using GrandTextAdventure.Core.TextProcessing;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core
{
    public class Tutorial
    {
        public static TutorialLine[] TutorialLines { get; set; } = new TutorialLine[] {
            new ("Lets go to north by typing 'north' or 'go north'", "go north", "Thank you"),
            new("Try to look around", "look around", "There is a Bycicle in south direction"),
            new("Use the Bycicle by entering", "enter Bycicle", "You are riding the Bicycle now") };

        public static void Start()
        {
            while (Settings.Instance.TutorialIndex < TutorialLines.Length)
            {
                var line = TutorialLines[Settings.Instance.TutorialIndex];
                Console.WriteLine(line.Description);
                var input = Console.ReadLine();
                var p = new TextProcessing.Parser();
                var cmd = p.ParseCommand(input);
                var expected = p.ParseCommand(line.ExpectedCommand);

                if (cmd.Noun == expected.Noun && cmd.Verb == expected.Verb) // ToDo need to fix
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
