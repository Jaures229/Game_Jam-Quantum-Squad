using UnityEngine;
using System.Linq;

public class TutorialStepManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialElement
    {
        [Tooltip("Liste des index d'étapes (1, 3, 5, etc.) où cet élément doit s'afficher.")]
        public int[] requiredSteps; // <<< NOUVEAU CHAMP : Tableau d'entiers >>>

        [Tooltip("L'objet UI ou l'élément interactif à activer/désactiver.")]
        public GameObject tutorialObject;

        // Vous pouvez supprimer 'isPersistent' car la liste gère la discontinuité.
    }

    [Header("Éléments Spécifiques à la Scène")]
    public TutorialElement[] sceneElements;

    private void Start()
    {
        // Si le tutoriel est terminé, désactivez tout immédiatement
        if (!GameStateManager.Instance.IsTutorialActive())
        {
            SetAllElementsState(false);
        }
        else
        {
            // Initialiser l'état des éléments au démarrage de la scène
            TutorialEvents.TutorialStarted("Tutorial_Intro");
            UpdateElementsVisibility(GameStateManager.Instance.currentTutorialStep);
        }
    }

    private void OnEnable()
    {
        TutorialEvents.OnTutorialStepAdvanced += UpdateElementsVisibility;
        TutorialEvents.OnTutorialEnded += HandleTutorialEnd;
    }

    private void OnDisable()
    {
        TutorialEvents.OnTutorialStepAdvanced -= UpdateElementsVisibility;
        TutorialEvents.OnTutorialEnded -= HandleTutorialEnd;
    }

    private void HandleTutorialEnd()
    {
        SetAllElementsState(false);
    }

    private void UpdateElementsVisibility(int currentStep)
    {
        foreach (var element in sceneElements)
        {
            if (element.tutorialObject == null) continue;

            // Utilisation de Linq: C'est propre et lisible.
            bool shouldBeActive = element.requiredSteps.Contains(currentStep);

            if (element.tutorialObject.activeSelf != shouldBeActive)
            {
                element.tutorialObject.SetActive(shouldBeActive);
            }
        }
    }

    private void SetAllElementsState(bool active)
    {
        foreach (var element in sceneElements)
        {
            if (element.tutorialObject != null)
            {
                element.tutorialObject.SetActive(active);
            }
        }
    }
}
