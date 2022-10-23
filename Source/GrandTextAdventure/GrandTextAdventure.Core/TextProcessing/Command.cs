using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core.TextProcessing;

public class Command
{

    public Command()
    {
        Verb = VerbCodes.NoCommand;
        Adjective = string.Empty;
        Noun = string.Empty;
        Preposition = PropositionEnum.NotRecognised;
        Adjective2 = string.Empty;
        Noun2 = string.Empty;
        Preposition2 = PropositionEnum.NotRecognised;
        Adjective3 = string.Empty;
        Noun3 = string.Empty;
        ProfanityDetected = false;
        Profanity = string.Empty;
    }


    public string Adjective { get; set; }


    public string Adjective2 { get; set; }


    public string Adjective3 { get; set; }


    public string FullTextCommand { get; set; }


    public string Noun { get; set; }


    public string Noun2 { get; set; }


    public string Noun3 { get; set; }


    public PropositionEnum Preposition { get; set; }


    public PropositionEnum Preposition2 { get; set; }

    public string Profanity { get; set; }


    public bool ProfanityDetected { get; set; }


    public VerbCodes Verb { get; set; }
}
