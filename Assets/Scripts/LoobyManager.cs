using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class LoobyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lobbyPanel; // Panel du lobby
    public GameObject stagePanel; // Panel de sélection des stages
    public TMP_Text stageButtonText;
    private string Stage_to_load = "Stage 01";
    void Start()
    {
        // Assure-toi que le lobby est affiché au départ
        lobbyPanel.SetActive(true);
        stagePanel.SetActive(false);
    }

    // Afficher le panel des stages
    public void ShowStagePanel()
    {
        lobbyPanel.SetActive(false);
        stagePanel.SetActive(true);
    }

    // Sélectionner un stage et mettre à jour le bouton du lobby
    public void SelectStage(string stageName)
    {
        stageButtonText.text = stageName;
        //stageButton.GetComponentInChildren<TextMeshPro>().text = stageName; // Mise à jour du texte
        Stage_to_load = stageName;
        stagePanel.SetActive(false); // Fermer le panel des stages
        lobbyPanel.SetActive(true);  // Retourner au lobby
    }

    public void Previous() {
        stagePanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void Play_Game()
    {
        if (Stage_to_load == "Stage 01")
        {
            lobbyPanel.SetActive(false);

        }
        if (Stage_to_load == "Stage 02") {
            lobbyPanel.SetActive(false);

        }
        if (Stage_to_load == "Stage 03") {
            lobbyPanel.SetActive(false);
        }
    }
}
