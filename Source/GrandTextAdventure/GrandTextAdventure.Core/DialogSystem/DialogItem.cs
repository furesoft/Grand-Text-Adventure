namespace GrandTextAdventure.Core.DialogSystem;

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
