namespace GrandTextAdventure.Core.Quests;

public interface IQuestTrigger
{
    bool IsCompleted(Quest quest);
}
