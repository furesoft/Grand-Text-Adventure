namespace GrandTextAdventure.Core
{
    public record DialogItem(string Name, string[] Lines, DialogItem Next, string[] ChooseLines = null)
    {

    }
}