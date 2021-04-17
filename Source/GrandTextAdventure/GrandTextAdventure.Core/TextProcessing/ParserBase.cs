using System.Text;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core.TextProcessing
{
    public class ParserBase
    {
        protected readonly IProfanityFilter ProfanityFilter = new ProfanityFilter();

        protected Command _command;

        protected ParserStatesEnum _parserStates = ParserStatesEnum.Verb;

        public ParserBase()
        {
            Verbs = new VerbSynonyms();
            Nouns = new NounSynonyms();
            Prepositions = new PrepositionMapping();
            Adjectives = new AdjectiveMapping();
            EnableProfanityFilter = true;
        }

        /// <summary>
        /// Constructor that allows you to custom set the verb, noun and preposition synonyms used by the parser. This
        /// constructor is mostly used by the unit tests.
        /// </summary>
        /// <param name="verbSynonyms">Verb synonyms to be used by the parser.</param>
        /// <param name="nounSynonyms">Noun synonyms to be used by the parser.</param>
        /// <param name="prepositionMapping">Prepositions being used by the parser.</param>
        public ParserBase(IVerbSynonyms verbSynonyms, INounSynonyms nounSynonyms, IPrepositionMapping prepositionMapping)
        {
            Verbs = verbSynonyms;
            Nouns = nounSynonyms;
            Prepositions = prepositionMapping;
            Adjectives = new AdjectiveMapping();
            EnableProfanityFilter = true;
        }

        /// <summary>
        /// Gets or sets the adjectives.
        /// </summary>
        /// <value>The adjectives.</value>
        public IAdjectiveMapping Adjectives { get; init; }

        /// <summary>
        /// If this flag is set to True, then the users input passed into the parser will also be scanned for profanity.
        /// It is not the intention of this engine to perform any censorship, but it can be useful to know if the player
        /// ius a potty mouthed little so and so. You can even use this fact as part of the narrative.
        /// </summary>
        public bool EnableProfanityFilter { get; set; }

        /// <summary>
        /// Retrieve the noun synonyms being used by the parser.
        /// </summary>
        public INounSynonyms Nouns { get; init; }

        /// <summary>
        /// Retrieve the prepositions being used by the parser.
        /// </summary>
        public IPrepositionMapping Prepositions { get; init; }

        /// <summary>
        /// Retrieve the verb synonyms being used by the parser.
        /// </summary>
        public IVerbSynonyms Verbs { get; init; }

        protected void CheckForProfanity(string lowerCase)
        {
            if (EnableProfanityFilter)
            {
                var profanity = ProfanityFilter.StringContainsFirstProfanity(lowerCase);
                if (!string.IsNullOrEmpty(profanity))
                {
                    _command.ProfanityDetected = true;
                    _command.Profanity = profanity;
                }
            }
        }

        protected string ProcessNoun(string word, ParserStatesEnum nextState)
        {
            var noun = Nouns.GetNounForSynonym(word);
            if (!string.IsNullOrEmpty(noun))
            {
                _parserStates = nextState;
                return noun;
            }

            //return string.Empty;
            return word;
        }

        protected PropositionEnum ProcessPreposition(string word, ParserStatesEnum nextState)
        {
            var preposition = Prepositions.GetPreposition(word);

            if (preposition != PropositionEnum.NotRecognised)
            {
                _parserStates = nextState;
                return preposition;
            }

            return PropositionEnum.NotRecognised;
        }

        protected VerbCodes ProcessVerbs(string word, ParserStatesEnum nextState)
        {
            var verb = Verbs.GetVerbForSynonym(word);

            if (verb != VerbCodes.NoCommand)
            {
                _parserStates = nextState;
            }

            if (verb != VerbCodes.NoCommand)
            {
                return verb;
            }

            return VerbCodes.NoCommand;
        }

        protected string RemovePunctuation(string s)
        {
            var result = new StringBuilder();

            for (var i = 0; i < s.Length; i++)
            {
                if (char.IsWhiteSpace(s[i]))
                {
                    result.Append(" ");
                }
                else if (char.IsLetter(s[i]) || char.IsNumber(s[i]))
                {
                    result.Append(s[i]);
                }
            }

            return result.ToString();
        }

        protected void SanitizeInput(string command, out string lowerCase, out string[] wordList)
        {
            lowerCase = command.ToLower();
            lowerCase = RemovePunctuation(lowerCase);

            wordList = lowerCase.Split(' ');
            _command = new Command();
        }
    }
}
