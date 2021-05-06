using GrandTextAdventure.Core.TextProcessing;

namespace GrandTextAdventure.Core
{
    public record TutorialLine(string Description, Command ExpectedCommand, string Output)
    {

    }
}
