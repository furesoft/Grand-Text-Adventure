namespace GrandTextAdventure.Core.Quests.Rewards;

public class ItemReward : IQuestReward
{
    public ItemReward(Inventory inventory)
    {
        Inventory = inventory;
    }

    public Inventory Inventory { get; set; }
    public void Invoke()
    {
        var state = GameEngine.Instance.GetState();
        Inventory.Transfer(state.Player.Inventory);

        GameEngine.Instance.SetState(state);
    }
}
