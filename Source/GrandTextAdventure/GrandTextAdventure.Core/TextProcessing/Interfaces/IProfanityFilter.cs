using System.Collections.ObjectModel;

namespace GrandTextAdventure.Core.TextProcessing.Interfaces
{
    public interface IProfanityFilter
    {
        ReadOnlyCollection<string> DetectAllProfanities(string sentence);

        bool IsProfanity(string word);

        string StringContainsFirstProfanity(string sentence);
    }
}
