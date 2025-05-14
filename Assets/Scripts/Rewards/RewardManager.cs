using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        QuestEvents.OnQuestCompleted += GrantReward;
    }

    void OnDestroy()
    {
        QuestEvents.OnQuestCompleted -= GrantReward;
    }

    void GrantReward(Quest quest)
    {
        if (quest.rewards == null)
        {
            Debug.LogWarning($"Pas de r�compense pour la qu�te : {quest.questTitle}");
            return;
        }

        Debug.Log($"R�compense de la qu�te {quest.questTitle} :");

        /*foreach (var reward in quest.rewards)
        {
            reward.ApplyReward();
        }

        if (quest.rewards.Count > 0)
        {
            QuestEvents.ApplyRewards?.Invoke("XP");
        } else {
            QuestEvents.ApplyRewards?.Invoke("None");
        }*/
    }
}
