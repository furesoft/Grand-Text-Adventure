using GrandTextAdventure.Core.TextProcessing;

namespace GrandTextAdventure.Core
{
    public class Tutorial
    {
        public TutorialLine[] TutorialLines { get; set; }



        public static void Start()
        {

        }

        public static bool IsTutorialStarted()
        {
            return Settings.Instance.WantPlayTutorial;
        }

        public static bool IsTutorialDone()
        {
            return Settings.Instance.TutorialDone;
        }

    }
}
