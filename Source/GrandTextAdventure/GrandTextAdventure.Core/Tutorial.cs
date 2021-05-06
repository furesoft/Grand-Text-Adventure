using System;

namespace GrandTextAdventure.Core
{
    public class Tutorial
    {
        public static TutorialLine[] TutorialLines { get; set; }

        public static void Start()
        {
            while (Settings.Instance.TutorialIndex < TutorialLines.Length)
            {
                var line = TutorialLines[Settings.Instance.TutorialIndex];
                Console.WriteLine(line.Description);
                var input = Console.ReadLine();
                var p = new TextProcessing.Parser();
                var cmd = p.ParseCommand(input);

                if (cmd.Equals(line.ExpectedCommand))
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
