using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestEvents
{
    public static Action<string> OnPlanetVisited;
    public static Action<string> OnEnemyDefeated;
    public static Action<string, string> OnPlanetStoneCollected;
    public static Action<Quest> OnQuestActivated;
    public static Action<Quest> OnQuestCompleted;
}
