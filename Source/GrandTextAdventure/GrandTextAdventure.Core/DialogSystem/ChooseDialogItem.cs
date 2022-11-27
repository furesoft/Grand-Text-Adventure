namespace GrandTextAdventure.Core.DialogSystem;

public class ChooseDialogItem : DialogItem
{
    public ChooseDialogItem(string title, string[] introductionLines, DialogItem[] children) : base(title)
    {
        IntroductionLines = introductionLines;
        Children = children;
    }

    public string[] IntroductionLines { get; set; }
    public DialogItem[] Children { get; set; }
}
