using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class QuestUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public TextMeshProUGUI questProgressionText;

    private Quest currentQuest;

    void Start()
    {
        currentQuest = QuestManager.Instance.currentQuest;

        if (currentQuest != null)
        {
            questTitleText.text = currentQuest.questTitle;
            questDescriptionText.text = currentQuest.description;

            // ?? Inscrire les callbacks pour chaque goal
            foreach (var goal in currentQuest.goals)
            {
                goal.OnProgressChanged += UpdateProgression;
            }

            UpdateProgression();
        }
    }

    void UpdateProgression()
    {
        if (currentQuest == null || currentQuest.goals == null) return;

        string combinedProgress = "";
        foreach (var goal in currentQuest.goals)
        {
            combinedProgress += "- " + goal.goalName + ": " + goal.GetProgression() + "\n";
        }

        questProgressionText.text = combinedProgress;
    }

    void OnDestroy()
    {
        if (currentQuest != null && currentQuest.goals != null)
        {
            foreach (var goal in currentQuest.goals)
            {
                goal.OnProgressChanged -= UpdateProgression;
            }
        }
    }
}
