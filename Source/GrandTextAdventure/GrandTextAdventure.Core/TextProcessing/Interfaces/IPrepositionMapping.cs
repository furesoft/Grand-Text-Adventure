using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core.TextProcessing.Interfaces
{
    public interface IPrepositionMapping
    {
        void Add(string inputProposition, PropositionEnum preposition);

        PropositionEnum GetPreposition(string preposition);
    }
}
