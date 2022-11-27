namespace GrandTextAdventure.Core.Quests.Rewards;

public class MoneyReward : IQuestReward
{
    public MoneyReward(Money rewardedMoney)
    {
        RewardedMoney = rewardedMoney;
    }

    public Money RewardedMoney { get; set; }

    public void Invoke()
    {
        var state = GameEngine.Instance.GetState();

        state.Player.Money += RewardedMoney;

        GameEngine.Instance.SetState(state);
    }
}
