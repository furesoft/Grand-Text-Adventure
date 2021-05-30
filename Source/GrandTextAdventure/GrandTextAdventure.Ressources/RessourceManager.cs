using System.IO;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;

namespace GrandTextAdventure.Ressources
{
    public static class RessourceManager
    {
        public static Stream StartSequenceDialog =>
                    typeof(RessourceManager).Assembly.GetManifestResourceStream("GrandTextAdventure.Ressources." + nameof(StartSequenceDialog) + ".json");
    }
}