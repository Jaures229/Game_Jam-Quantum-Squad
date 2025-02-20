using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private static SceneLoader instance;
    [SerializeField] private string _sceneToLoad;
    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLoading()
    {
        StartCoroutine(LoadSceneMainScene());
    }

    IEnumerator LoadSceneMainScene()
    {
        SceneManager.LoadScene("LoadingScene");
        yield return null;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        float minLoadingTime = 5f;
        float timer = 0f;

        while (!asyncLoad.isDone)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            //Debug.Log("Chargement: " + (progress * 100) + "%");

            if (asyncLoad.progress >= 0.9f && timer >= minLoadingTime)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
        Debug.Log("Game Loaded !");
    }
    //void Update()
    //{
        
    //}
}
