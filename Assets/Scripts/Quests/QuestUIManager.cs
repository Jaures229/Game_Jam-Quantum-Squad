using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentContainer;           // Le Content de ta Scroll View
    public GameObject questUIPrefab;             // Ton prefab QuestUIItem
    public TextMeshProUGUI Current_Quest_text;

    private List<Quest> quests;                  // Toutes les quêtes à afficher (à adapter selon ton système)
    private void Start()
    {
        // Exemple d'initialisation (tu peux remplacer ça par une vraie source de données)
        quests = QuestManager.Instance.GetAllQuests();
        PopulateQuestList();
    }

    public void PopulateQuestList()
    {
        // Nettoie d'abord les anciens éléments
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        // Crée un item pour chaque quête
        foreach (Quest quest in quests)
        {
            GameObject item = Instantiate(questUIPrefab, contentContainer);
            QuestUIItem ui = item.GetComponent<QuestUIItem>();
            ui.Setup(quest, OnQuestSelected);
        }
    }

    private void OnQuestSelected(Quest selectedQuest)
    {
        Debug.Log("Quête sélectionnée : " + selectedQuest.questTitle);
        Current_Quest_text.text = "Prochaine quête: " + selectedQuest.questTitle;

        QuestManager.Instance.SetCurrentQuest(selectedQuest);
        QuestManager.Instance.StartQuest(selectedQuest);
        QuestEvents.OnQuestActivated?.Invoke(selectedQuest);
    }
}
