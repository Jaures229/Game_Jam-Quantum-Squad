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
        public bool isPermanentElement = false;

        // Vous pouvez supprimer 'isPersistent' car la liste gère la discontinuité.
    }

    [Header("Éléments Spécifiques à la Scène")]
    public TutorialElement[] sceneElements;
    public string Tutorial_name = "Tutorial_Intro";

    private void Start()
    {
        // Si le tutoriel est terminé, désactivez tout immédiatement
        if (!GameStateManager.Instance.IsTutorialActive())
        {
            //SetAllElementsState(false);
            SetInitialStateOnLoad(false);
        }
        else
        {
            // Initialiser l'état des éléments au démarrage de la scène
            TutorialEvents.TutorialStarted(Tutorial_name);
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

    private void HandleTutorialEnd(string TutorialNameId)
    {
        //TutorialEvents.TutorialEnded(TutorialNameId);
        CleanupTutorialElements();
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

    /// <summary>
    /// Désactive tous les éléments du tutoriel, sauf ceux marqués comme permanents.
    /// </summary>
    private void CleanupTutorialElements()
    {
        foreach (var element in sceneElements)
        {
            if (element.tutorialObject == null) continue;

            // Si l'élément est marqué comme permanent, on ne le touche pas, 
            // car il est censé rester actif pour le jeu.
            if (element.isPermanentElement)
            {
                continue;
            }

            // Si l'élément n'est PAS permanent, on le désactive.
            element.tutorialObject.SetActive(false);
        }
    }

    // <summary>
    /// Nouvelle fonction d'initialisation pour le chargement.
    /// Elle désactive seulement les objets NON permanents si le tutoriel est fini.
    /// </summary>
    private void SetInitialStateOnLoad(bool active)
    {
        foreach (var element in sceneElements)
        {
            if (element.tutorialObject != null)
            {
                if (element.isPermanentElement)
                {
                    // Les éléments permanents sont initialisés à l'état inverse s'ils doivent être cachés
                    // AU DÉBUT du jeu avant le tutoriel, ou à TRUE s'ils sont toujours là.
                    // Par défaut, s'ils sont permanents, ils devraient être gérés par l'UpdateElementsVisibility() pour l'affichage initial.
                    if (GameStateManager.Instance.IsTutorialActive() == false)
                    {
                        // Si le jeu démarre APRÈS le tutoriel, tous les éléments permanents 
                        // requis pour les premières étapes DOIVENT être actifs.
                        if (element.requiredSteps.Min() <= GameStateManager.Instance.currentTutorialStep)
                        {
                            element.tutorialObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    // Désactivation des éléments de tutoriel temporaires
                    element.tutorialObject.SetActive(active);
                }
            }
        }
    }
}
