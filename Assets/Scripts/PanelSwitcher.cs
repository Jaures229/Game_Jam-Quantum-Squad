using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject[] panels; // Liste des panels à assigner dans l'inspecteur
    public GameObject finish_panel;
    public GameObject Explore_button;
    public GameObject Finish_button;

    public GameObject game_panel;
    [SerializeField] private GameObject load;
    private int currentIndex = 0;

    void Start()
    {
        // Désactiver tous les panels sauf le premier
        load.SetActive(false);
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }
    }

    public void NextPanel()
    {
        if (currentIndex < panels.Length -1)
        {
            panels[currentIndex].SetActive(false); // Désactive le panel actuel
            currentIndex++;
            panels[currentIndex].SetActive(true); // Active le panel suivant
        } else {
            panels[currentIndex].SetActive(false);
            Explore_button.SetActive(true);
            Finish_button.SetActive(true);
            // finish_panel.SetActive(false);
            // game_panel.SetActive(true);
        }
    }

    public void Transi()
    {
        load.SetActive(true);
    }
}
