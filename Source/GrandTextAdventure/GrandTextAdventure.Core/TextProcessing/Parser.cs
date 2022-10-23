using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core.TextProcessing;

/// <summary>
/// The parser is the system that takes the players written input and reduces the synonyms down to command
/// instances that the game can interpret.
///
/// The parser process tries to split the input down into the following forms.
/// verb - noun
/// verb - noun - preposition - noun
/// </summary>
public class Parser : ParserBase
{
    /// <summary>
    /// Default constructor that sets the default initial state of the parser.
    /// </summary>
    public Parser()
    {
    }


    public Parser(IVerbSynonyms verbSynonyms, INounSynonyms nounSynonyms, IPrepositionMapping prepositionMapping) : base(verbSynonyms, nounSynonyms, prepositionMapping)
    {
    }

    /// <summary>
    /// This is the method that will take a users input and parse it into a game command.
    ///
    /// The parser process tries to split the input down into the following forms.
    /// verb - noun
    /// verb - noun - preposition - noun
    ///
    /// The command that is returned by this method will reduce any verb and noun synonyms down into a basic set of
    /// default verb and nouns that the game logic can easily react too. This means if the player types any of the following, then
    /// would be mapped to the same command.
    ///
    /// Get Key
    /// Grab Key
    /// Pickup key
    ///
    /// </summary>
    /// <param name="command">A string representing the command types in by the user.</param>
    /// <returns>An instance of a command that is passed back to the controlling room for processing.</returns>
    public Command ParseCommand(string command)
    {
        if (string.IsNullOrEmpty(command))
        {
            var toReturn = new Command
            {
                FullTextCommand = string.Empty
            };

            return toReturn;
        }

        SanitizeInput(command, out var lowerCase, out var wordList);
        CheckForProfanity(lowerCase);

        return ReduceInputToCommand(lowerCase, wordList);
    }

    private void MultiWordCommand(string[] commandList)
    {
        // This doesn't appear to be working, try:
        // look at the hat
        foreach (var word in commandList)
        {
            switch (_parserStates)
            {
                case ParserStatesEnum.Verb:
                    var verb = ProcessVerbs(word, ParserStatesEnum.Noun);

                    if (verb == VerbCodes.NoCommand) { continue; }

                    _command.Verb = verb;
                    break;

                case ParserStatesEnum.Noun:
                    if (Adjectives.CheckAdjectiveExists(word))
                    {
                        _command.Adjective = word;
                        continue;
                    }

                    var noun = ProcessNoun(word, ParserStatesEnum.Preposition);

                    if (noun?.Length == 0) continue;
                    _command.Noun = noun;
                    break;

                case ParserStatesEnum.Preposition:
                    var preposition = ProcessPreposition(word, ParserStatesEnum.Noun2);

                    if (preposition == PropositionEnum.NotRecognised) { continue; }
                    _command.Preposition = preposition;
                    break;

                case ParserStatesEnum.Noun2:
                    if (Adjectives.CheckAdjectiveExists(word))
                    {
                        _command.Adjective2 = word;
                        continue;
                    }

                    var noun2 = ProcessNoun(word, ParserStatesEnum.Preposition2);

                    if (noun2?.Length == 0) continue;
                    _command.Noun2 = noun2;
                    break;

                case ParserStatesEnum.Preposition2:
                    var preposition2 = ProcessPreposition(word, ParserStatesEnum.Noun3);
                    if (preposition2 == PropositionEnum.NotRecognised) continue;
                    _command.Preposition2 = preposition2;
                    break;

                case ParserStatesEnum.Noun3:
                    if (Adjectives.CheckAdjectiveExists(word))
                    {
                        _command.Adjective3 = word;
                        continue;
                    }

                    var noun3 = ProcessNoun(word, ParserStatesEnum.None);

                    if (noun3?.Length == 0) continue;
                    _command.Noun3 = noun3;
                    break;
            }
        }

        _parserStates = ParserStatesEnum.Verb;
    }

    private Command ReduceInputToCommand(string lowerCase, string[] wordList)
    {
        switch (wordList.Length)
        {
            case 0:
                return new Command();

            case 1:
                SingleWordCommand(wordList[0]);
                _command.FullTextCommand = lowerCase;
                return _command;

            default:
                MultiWordCommand(wordList);
                _command.FullTextCommand = lowerCase;
                return _command;
        }
    }

    private void SingleWordCommand(string command)
    {
        var direction = DirectionsHelper.GetDirectionCommand(command);

        _command.Verb = direction.Verb;
        _command.Noun = direction.Noun;
        _command.FullTextCommand = direction.FullTextCommand;

        if (_command.Verb == VerbCodes.NoCommand)
        {
            _command.Verb = Verbs.GetVerbForSynonym(command);
        }
    }
}
