using System.IO;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;

namespace GrandTextAdventure.Ressources
{
    public static class RessourceManager
    {
        public static Stream StartSequenceDialog =>
                    LoadStream(nameof(StartSequenceDialog) + ".json");

        public static Stream SampleEntities => LoadStream("samples.ced");

        private static Stream LoadStream(string name)
        {
            return typeof(RessourceManager).Assembly.GetManifestResourceStream("GrandTextAdventure.Ressources." + name);
        }
    }
}