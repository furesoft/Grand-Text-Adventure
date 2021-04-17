using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.TextProcessing.Interfaces;

namespace GrandTextAdventure.Core.TextProcessing.Synonyms
{
    public enum PropositionEnum
    {
        NotRecognised = 0,
        Into = 1,
        Against = 2,
        To = 3,
        In = 4,
        On = 5,
        Through = 6,
        Over = 7,
        Under = 8,
        Across = 9,
        Behind = 10,
        At = 11,
        Up = 12,
        From = 12,
        With = 13
    }

    /// <summary>
    /// Mapping of strings to prepositions used by the parser.
    /// </summary>
    public class PrepositionMapping : IPrepositionMapping
    {
        private readonly Dictionary<string, PropositionEnum> _prepositionMappings = new();

        /// <summary>
        /// Constructor that loads in the initial supported prepositions.
        /// </summary>
        public PrepositionMapping()
        {
            Add("into", PropositionEnum.Into);
            Add("against", PropositionEnum.Against);
            Add("to", PropositionEnum.To);
            Add("in", PropositionEnum.In);
            Add("on", PropositionEnum.On);

            Add("through", PropositionEnum.Through);
            Add("over", PropositionEnum.Over);
            Add("under", PropositionEnum.Under);
            Add("across", PropositionEnum.Across);
            Add("behind", PropositionEnum.Behind);
            Add("at", PropositionEnum.At);
            Add("up", PropositionEnum.Up);
            Add("from", PropositionEnum.From);
            Add("with", PropositionEnum.With);
        }

        /// <summary>
        /// Add a preposition mapping.
        /// </summary>
        /// <param name="inputProposition">String to map to a preposition.</param>
        /// <param name="preposition">The preposition being mapped.</param>
        /// <exception cref="ArgumentNullException">If the preposition being mapped is null or empty, then throw an
        /// ArgumentNullException.</exception>
        public void Add(string inputProposition, PropositionEnum preposition)
        {
            if (string.IsNullOrEmpty(inputProposition))
            {
                throw new ArgumentNullException(nameof(inputProposition));
            }

            _prepositionMappings.Add(inputProposition, preposition);
        }

        /// <summary>
        /// Get a preposition enum based on an input mapping string.
        /// </summary>
        /// <param name="preposition">The string to map.</param>
        /// <returns>The enum entry for the mapped preposition.</returns>
        /// <exception cref="ArgumentNullException">If the preposition being mapped is null or empty, then throw an
        /// ArgumentNullException.</exception>
        public PropositionEnum GetPreposition(string preposition)
        {
            if (string.IsNullOrEmpty(preposition))
            {
                return PropositionEnum.NotRecognised;
            }

            if (_prepositionMappings.ContainsKey(preposition))
            {
                return _prepositionMappings[preposition];
            }
            else
            {
                return PropositionEnum.NotRecognised;
            }
        }
    }
}
