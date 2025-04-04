using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;
using UnityEngine.SceneManagement;

public class LoobyManager : MonoBehaviour
{
    public GameObject lobbyPanel;
    public GameObject stagePanel;
    [SerializeField] private GameObject _transiPanel;
    [SerializeField] private Animator animator;
    public TMP_Text stageButtonText;
    private string Stage_to_load = "Stage 01";
    void Start()
    {
        
        lobbyPanel.SetActive(true);
        stagePanel.SetActive(false);
        _transiPanel.SetActive(false);
    }

    public void ShowStagePanel()
    {
        lobbyPanel.SetActive(false);
        stagePanel.SetActive(true);
    }

    public void SelectStage(string stageName)
    {
        stageButtonText.text = stageName;
        //stageButton.GetComponentInChildren<TextMeshPro>().text = stageName; // Mise à jour du texte
        Stage_to_load = stageName;
        stagePanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void Previous() {
        stagePanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    IEnumerator LoadSceneWithAnimation(string sceneName)
    {
        animator.SetTrigger("Load");

        yield return new WaitForSeconds(15f);

        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetString("_currentScene", sceneName);
    }

    public void Play_Game()
    {
        if (Stage_to_load == "Stage 01")
        {
            lobbyPanel.SetActive(false);
            _transiPanel.SetActive(true);
            StartCoroutine(LoadSceneWithAnimation("MainScene1"));
            //SceneManager.LoadScene("MainScene1");
        }
        if (Stage_to_load == "Stage 02") {
            lobbyPanel.SetActive(false);
            _transiPanel.SetActive(true);
            StartCoroutine(LoadSceneWithAnimation("MainScene2"));
            //SceneManager.LoadScene("MainScene2");
        }
        if (Stage_to_load == "Stage 03") {
            lobbyPanel.SetActive(false);
            _transiPanel.SetActive(true);
            StartCoroutine(LoadSceneWithAnimation("Mars"));
            //SceneManager.LoadScene("Mars");
        }
    }
}
