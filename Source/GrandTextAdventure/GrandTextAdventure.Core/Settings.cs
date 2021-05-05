namespace GrandTextAdventure.Core
{
    public class Settings
    {
        private static Settings s_instance;
        public static Settings Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new Settings();
                }

                return s_instance;
            }
        }

        public bool WantPlayTutorial { get; set; }
        public bool TutorialDone { get; set; }

        public int TutorialIndex { get; set; }
    }
}
