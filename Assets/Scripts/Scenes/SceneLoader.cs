using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;
    public TextMeshProUGUI progressText; // ou TMP_Text si tu utilises TextMeshPro

    [Header("Paramètres")]
    public float fakeSpeed = 0.3f; // vitesse simulée pour un effet fluide
    public float minLoadingTime = 2f; // temps minimum de chargement pour le style

    private float targetProgress = 0f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {

        yield return new WaitForSeconds(0.3f); // petit délai esthétique

        // 2️⃣ Commence le chargement
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            // Unity charge réellement jusqu’à 0.9 avant d’activer la scène
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Lissage de la progression pour que ça soit doux
            targetProgress = Mathf.MoveTowards(targetProgress, realProgress, Time.deltaTime * fakeSpeed);

            progressBar.value = targetProgress;
            progressText.text = Mathf.RoundToInt(targetProgress * 100f) + "%";

            // 3️⃣ Quand c’est prêt et que le temps minimum est écoulé → on active la scène
            if (targetProgress >= 1f && operation.progress >= 0.9f && timer > minLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
