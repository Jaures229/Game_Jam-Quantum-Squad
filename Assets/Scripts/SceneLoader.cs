using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    private Animator animator;

    [SerializeField] private List<GameObject> objectsToDisable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartSceneTransition(string sceneName)
    {
        StartCoroutine(LoadSceneWithAnimation(sceneName));
    }

    IEnumerator LoadSceneWithAnimation(string sceneName)
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null) obj.SetActive(false);
        }
        animator.SetTrigger("Load");

        yield return new WaitForSeconds(15f);

        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetString("_currentScene", sceneName);
    }
}
