using System;

namespace GrandTextAdventure
{
    internal class AutoCompletionHandler : IAutoCompleteHandler
    {
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/' };

        public string[] GetSuggestions(string text, int index)
        {
            return new[] { "north", "south", "suicide", "west", "east", "enter", "leave", "go", "look", "around" };
        }
    }
}
