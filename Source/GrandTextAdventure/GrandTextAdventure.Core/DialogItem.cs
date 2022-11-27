using Darlek.Scheme;

namespace GrandTextAdventure.Core;

public abstract class DialogItem
{
    protected DialogItem(string title, DialogItem next = null)
    {
        Title = title;
        Next = next;
    }

    public DialogItem Parent { get; set; }
    public string Title { get; set; }
    public DialogItem Next { get; set; }
}

public class TextDialogItem : DialogItem
{
    public TextDialogItem(string title) : base(title)
    {
    }
}

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

public abstract class ActionDialogItem : DialogItem
{
    protected ActionDialogItem(string title, DialogItem next = null) : base(title, next)
    {
    }

    public abstract void Invoke();
}

public class CallableActionDialogItem : ActionDialogItem
{
    public CallableActionDialogItem(string title, ICallable callback, DialogItem next = null) : base(title, next)
    {
        Callback = callback;
    }

    public ICallable Callback { get; set; }

    public override void Invoke()
    {
        Callback.Call(new() { Parent });
    }
}
