using UnityEngine;
using TMPro;

public class QuestUIUpdate : MonoBehaviour
{
    public TextMeshProUGUI questStatusText;

    private void OnEnable()
    {
        QuestEvents.OnQuestAccepted += UpdateUI;
        QuestEvents.OnQuestCompleted += UpdateUI;
        QuestEvents.OnObjectiveUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        QuestEvents.OnQuestAccepted -= UpdateUI;
        QuestEvents.OnQuestCompleted -= UpdateUI;
        QuestEvents.OnObjectiveUpdated -= UpdateUI;
    }

    private void UpdateUI(Quest quest)
    {
        // Surcharge pour les événements OnQuestAccepted et OnQuestCompleted
        UpdateUI(quest, null);
    }

    private void UpdateUI(Quest quest, QuestObjective objective)
    {
        if (questStatusText == null) return;

        string status = $"Quête: {quest.questName}\n";
        status += $"Description: {quest.description}\n";

        if (quest.IsCompleted())
        {
            status += "Statut: TERMINÉ - Rendez-vous au PNJ !\n";
        }
        else
        {
            status += "Statut: EN COURS\n";
            status += "Objectifs:\n";
            foreach (var obj in quest.objectives)
            {
                status += $"- {obj.GetProgressText()}\n";
            }
        }

        questStatusText.text = status;
    }
}

