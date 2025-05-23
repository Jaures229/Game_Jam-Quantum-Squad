using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{

    [SerializeField] private GameObject _panelStartAnimation;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _transiGame;
    [SerializeField] private GameObject _continueGame;
    //[SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private AudioClip TouchingSound;
    [SerializeField] private AudioClip buttonClickSound;


    private Animator _animatorIntro;

    void Start()
    {
        _menuPanel.SetActive(false);
        _transiGame.SetActive(false);
        _animatorIntro = _panelStartAnimation.GetComponent<Animator>();
        PlayerPrefs.SetString("_currentScene", "MainScene1");
        if (PlayerPrefs.GetInt("_hasSeeIntro", 0) == 1)
        {
            _panelStartAnimation.SetActive(false);
            _menuPanel.SetActive(true);

        }
        else
        {
            StartCoroutine(PlayIntro());

        }
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(10f);

        _animatorIntro.SetBool("part2", true);

        yield return new WaitUntil(() => IsTouching());

        _animatorIntro.SetBool("part2", false);
        _animatorIntro.SetBool("part3", true);

        yield return new WaitForSeconds(4f);

        _panelStartAnimation.SetActive(false);
        _menuPanel.SetActive(true);
        if (PlayerPrefs.GetInt("_hasAlreadyPlay", 0) == 1)
        {
            _continueGame.SetActive(true);
        }
        else
        {
            _continueGame.SetActive(false);
        }
        PlayerPrefs.SetInt("_hasSeeIntro", 1);
    }

    bool IsTouching()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            AudioManager.Instance.PlaySFX(TouchingSound);
            return true;
        }
        return false;
    }

    public void Trasi ()
    {
        _transiGame.SetActive(true);
    }

    public void LeaveGame ()
    {
        Application.Quit();
        PlayerPrefs.SetInt("_hasSeeIntro", 0);
    }
    void Update()
    {
        
    }
}
