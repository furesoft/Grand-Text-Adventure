using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core.TextProcessing.Interfaces
{
    public interface IVerbSynonyms
    {
        void Add(string synonym, VerbCodes verb);

        VerbCodes GetVerbForSynonym(string synonym);
    }
}
