using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("UI Références")]
    public GameObject loadingScreen;   // Le panel du loading screen
    public Slider progressBar;         // La barre de chargement
    public Text progressText;          // (optionnel) pour afficher le %

    [Header("Paramètres")]
    public string sceneToLoad;         // La scène à charger (ex: "GameScene")

    // Fonction appelée par ton bouton "New Game"
    public void StartNewGame()
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

        // Commence à charger la scène en arrière-plan
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Empêche d’activer direct la scène

        while (!operation.isDone)
        {
            // Progression (Unity retourne [0,0.9] pendant le chargement)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            progressBar.value = progress;

            if (progressText != null)
                progressText.text = (progress * 100f).ToString("F0") + "%";

            // Quand c’est presque fini → active la scène
            if (progress >= 1f)
            {
                yield return new WaitForSeconds(0.5f); // petit délai optionnel
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
