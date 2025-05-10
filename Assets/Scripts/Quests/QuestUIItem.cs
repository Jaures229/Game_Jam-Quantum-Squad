using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIItem : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Button goButton;

    private Quest questData;

    public void Setup(Quest quest, System.Action<Quest> onClick)
    {
        questData = quest;
        titleText.text = quest.questTitle;
        descriptionText.text = quest.description;

        goButton.onClick.RemoveAllListeners();
        goButton.onClick.AddListener(() => onClick?.Invoke(questData));
    }
}
