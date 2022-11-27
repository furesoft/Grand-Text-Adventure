namespace GrandTextAdventure.Core.DialogSystem;

public abstract class ActionDialogItem : DialogItem
{
    protected ActionDialogItem(string title, DialogItem next = null) : base(title, next)
    {
    }

    public abstract void Invoke();
}
