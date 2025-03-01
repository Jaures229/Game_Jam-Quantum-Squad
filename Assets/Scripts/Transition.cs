using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Transition : MonoBehaviour
{

    [SerializeField] private GameObject _stagesPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _menuPanel;
    //private Animator anim;

    private void Start()
    {
        if (_stagesPanel != null) 
        {
            _stagesPanel.SetActive(false);
        }
    }
    public void TransiToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    public void BackToMenu (GameObject _currentPanel)
    {
        _menuPanel.SetActive(true);
        _currentPanel.SetActive(false);
    }

    public void TransiToCurrentStage()
    {
        //SceneManager.LoadScene(PlayerPrefs.GetString("_currentScene"));
        SceneManager.UnloadSceneAsync("Menu");
    }

    public void TransiToOption ()
    {
        _optionsPanel.SetActive(true);
        _menuPanel.SetActive(false);
    }

    public void TransiToStages ()
    {
        _stagesPanel.SetActive(true);
        _menuPanel.SetActive(false);
    }

    public void PressButton (Animator anim)
    {
        anim.SetTrigger("Pressed");
    }

    public void TransiToExit()
    {
        Application.Quit();
    }
}
