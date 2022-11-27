using System;
using System.Collections.Generic;

namespace GrandTextAdventure.Core.Quests;

public class Quest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Issuer { get; set; } //The npc who gave the quest
    public List<IQuestReward> Rewards { get; set; } = new();
    public List<IQuestTrigger> Triggers { get; set; } = new(); //triggers, like killing npcs

    public Dictionary<string, object> Storage { get; set; } = new();
}
