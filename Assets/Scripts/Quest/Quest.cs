// using UnityEngine;
// using System.Collections.Generic;

// [CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
// public class Quest : ScriptableObject
// {
//     public string questName = "Nouvelle Quête";
//     [TextArea(3, 10)]
//     public string description = "Description de la quête.";
//     public List<QuestObjective> objectives = new List<QuestObjective>();
//     public List<QuestReward> rewards = new List<QuestReward>(); 
//     public bool IsCompleted()
//     {
//         foreach (QuestObjective objective in objectives)
//         {
//             if (!objective.IsCompleted())
//             {
//                 return false;
//             }
//         }
//         return true;
//     }

//     public void ResetProgress()
//     {
//         foreach (QuestObjective objective in objectives)
//         {
//             objective.ResetObjective();
//         }
//     }

//     public void ApplyAllRewards()
//     {
//         foreach (QuestReward reward in rewards)
//         {
//             reward.ApplyReward();
//         }
//     }
// }


using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    [Header("Informations Générales")]
    public string questID;
    public string questName = "Nouvelle Quête";
    [TextArea(3, 10)]
    public string description = "Description de la quête.";

    [Header("Objectifs et Récompenses")]
    public List<QuestObjective> objectives = new List<QuestObjective>();
    public List<QuestReward> rewards = new List<QuestReward>(); 

    [Header("Dépendances")]
    [Tooltip("Indique si cette quête débloque une autre quête à sa complétion.")]
    public bool unlocksAnotherQuest = false;
    [Tooltip("La quête qui sera débloquée si 'unlocksAnotherQuest' est vrai.")]
    public Quest questToUnlock; 

    // Variable pour gérer l'état de verrouillage par défaut de la quête.
    // Cette valeur est le "blueprint", elle sera copiée dans QuestState au démarrage.
    public bool isLocked = true; 

    /// <summary>
    /// Vérifie si tous les objectifs de la quête sont terminés.
    /// </summary>
    public bool IsCompleted()
    {
        foreach (QuestObjective objective in objectives)
        {
            if (!objective.IsCompleted())
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Réinitialise la progression de tous les objectifs de la quête.
    /// </summary>
    public void ResetProgress()
    {
        foreach (QuestObjective objective in objectives)
        {
            objective.ResetObjective();
        }
    }

    /// <summary>
    /// Applique toutes les récompenses de la quête.
    /// </summary>
    public void ApplyAllRewards()
    {
        foreach (QuestReward reward in rewards)
        {
            reward.ApplyReward();
        }
    }
}