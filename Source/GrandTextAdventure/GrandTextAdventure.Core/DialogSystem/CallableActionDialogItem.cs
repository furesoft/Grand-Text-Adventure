using Darlek.Scheme;

namespace GrandTextAdventure.Core.DialogSystem;

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
