namespace GrandTextAdventure.Core.TextProcessing.Interfaces;

public interface INounSynonyms
{
    void Add(string synonym, string noun);

    string GetNounForSynonym(string synonym);
}
