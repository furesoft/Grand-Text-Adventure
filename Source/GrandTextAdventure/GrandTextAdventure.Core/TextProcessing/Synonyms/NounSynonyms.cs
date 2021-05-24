using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.TextProcessing.Interfaces;

namespace GrandTextAdventure.Core.TextProcessing.Synonyms
{
    /// <summary>
    /// The NounSynonyms class handles the mapping of many synonyms to a single verb. This means the parser can map and
    /// reduce different nouns entered by the player to a series of single nouns that can be used for triggering different
    /// events in a game.
    /// </summary>
    public class NounSynonyms : INounSynonyms
    {
        private readonly Dictionary<string, string> _synonymsMappings = new();

        /// <summary>
        /// Constructor that setus up the default noun encoding for different navigation directions.
        /// </summary>
        public NounSynonyms()
        {
            Add("n", "north");
            Add("s", "south");
            Add("e", "east");
            Add("w", "west");
            Add("u", "up");
            Add("d", "down");

            Add("ne", "northeast");
            Add("se", "southeast");
            Add("sw", "southwest");
            Add("nw", "northwest");

            Add("northeast", "northeast");
            Add("southeast", "southeast");
            Add("southwest", "southwest");
            Add("northwest", "northwest");

            Add("north", "north");
            Add("south", "south");
            Add("east", "east");
            Add("west", "west");
            Add("up", "up");
            Add("down", "down");

            Add("forward", "north");
            Add("backward", "south");
            Add("forwards", "north");
            Add("backwards", "south");

            Add("f", "north");
            Add("b", "south");
            Add("right", "east");
            Add("left", "west");
            Add("r", "east");
            Add("l", "west");
        }

        /// <summary>
        /// Add a new synonym mapping into the dictionary.
        /// </summary>
        /// <param name="synonym">The synonym to be mapped.</param>
        /// <param name="noun">The noun that the synonym maps onto.</param>
        /// <exception cref="ArgumentNullException">If either the synonym or noun inputs are null of empty, an
        /// ArgumentNullException is thrown.</exception>
        public void Add(string synonym, string noun)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                return;
            }

            if (string.IsNullOrEmpty(noun))
            {
                return;
            }

            _synonymsMappings.Add(synonym, noun);
        }

        /// <summary>
        /// Return the base noun by providing one of it's synonyms. This is used by the parser to reduce potential
        /// synonyms down to the base noun to add into a command
        /// </summary>
        /// <param name="synonym">The synonym to return a noun for.</param>
        /// <returns>The mapped noun</returns>
        /// <exception cref="ArgumentNullException">If the synonym is null or empty throw an ArgumentNullException.</exception>
        public string GetNounForSynonym(string synonym)
        {
            if (string.IsNullOrEmpty(synonym))
            {
                return string.Empty;
            }

            return _synonymsMappings.ContainsKey(synonym) ? _synonymsMappings[synonym] : string.Empty;
        }
    }
}
