using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{

    [SerializeField] private GameObject _panelStartAnimation;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _continueGame;
    [SerializeField] private AudioSource backgroundMusic;
    private Animator _animatorIntro;
    private bool _canStartGame = false;

    void Start()
    {
        _menuPanel.SetActive(false);
        PlayBackgroundMusic();
        _animatorIntro = _panelStartAnimation.GetComponent<Animator>();
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

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true; // Assure que la musique se joue en boucle
            backgroundMusic.Play();
        }
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(10f);

        _animatorIntro.SetBool("part2", true);

        yield return new WaitUntil(() => IsTouching());

        _animatorIntro.SetBool("part2", false);
        _animatorIntro.SetBool("part3", true);

        yield return new WaitForSeconds(5f);

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
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

   

    public void LeaveGame ()
    {
        //Debug.Log("Le jeu se ferme !");
        Application.Quit();
    }
    void Update()
    {
        
    }
}
