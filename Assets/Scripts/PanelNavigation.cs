using UnityEngine;
using UnityEngine.UI;

public class PanelNavigation : MonoBehaviour
{
    public GameObject[] panels; // Liste des panels à naviguer
    private int currentIndex = 0;
    //public GameObje
    void Start()
    {
        UpdatePanels();
    }

    public void NextPanel()
    {
        if (currentIndex < panels.Length - 1)
        {
            currentIndex++;
            UpdatePanels();
        }
    }

    public void PreviousPanel()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdatePanels();
        }
    }

    private void UpdatePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentIndex);
        }
    }
}
