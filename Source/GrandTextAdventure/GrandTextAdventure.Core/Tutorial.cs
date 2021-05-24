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
            new("Drive the Bycicle", "drive Bycicle", "You are riding the Bicycle now"),
            new("See the Car behind you? Crack it by entering", "enter car", "You are awesome"),
            new("Now you can use your MG11 Weapon, shoot it and don't die!", "shoot MG11", "NPC died. You got 250 $."),
            new("You got money, so why not let show how much you actually have? Type dollars and you'll find it out.", "dollars", "You have 733 $ and 2 Pilzschafcoins"),
            new("Now you know how to play. You can type what you want, the specific keywords must be present, like you learned before. Type play to start the game.", "play", "") };

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

            Settings.Instance.TutorialDone = true;

            Settings.Save();
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
