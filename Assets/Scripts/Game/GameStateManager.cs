using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [Header("États de la Chronologie")]
    public bool isTutorialActive = true; // Par défaut, le tutoriel est actif au début du jeu
    public bool isFirstQuestAccepted = false;
    public bool isAct2Unlocked = false;

    [Header("Référence à la Quête Actuelle")]
    public Quest currentMainQuest; // Référence au ScriptableObject de la quête principale en cours
    public string Tutorial_name;
    void OnEnable()
    {
        // ABONNEMENT : On ajoute la méthode à l'événement
        DialogueManager.OnDialogueStart += CheckStartDialogue;
        DialogueManager.OnDialogueEnd += CheckEndDialogue;

        //
        TutorialEvents.OnObjectiveAwaiting += CheckWaitingTutorial;


        TutorialEvents.OnTutorialStarted += TutorialStarted;
        TutorialEvents.OnTutorialEnded += TutorialEnded;

        TutorialEvents.OnTutorialObjectiveCompleted += CheckedFinieshedObjective;
    }

    void OnDisable()
    {
        // DÉSABONNEMENT : Essentiel pour éviter les fuites de mémoire et les erreurs !
        DialogueManager.OnDialogueStart -= CheckStartDialogue;
        DialogueManager.OnDialogueEnd -= CheckEndDialogue;

        TutorialEvents.OnObjectiveAwaiting -= CheckWaitingTutorial;
        TutorialEvents.OnTutorialObjectiveCompleted -= CheckedFinieshedObjective;

        TutorialEvents.OnTutorialStarted -= TutorialStarted;
        TutorialEvents.OnTutorialEnded -= TutorialEnded;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Progression du Tutoriel")]
    // Index de l'étape actuelle du tutoriel. 0 signifie "Tutoriel terminé".
    public int currentTutorialStep = 1; 
    
    // Définissez un nombre total d'étapes, par exemple:
    public const int TUTORIAL_STEP_END = 6; 

    public bool IsTutorialActive()
    {
        // Le tutoriel est actif si l'étape actuelle est > 0
        return currentTutorialStep > 0;
    }

    public void AdvanceTutorialStep()
    {
        // Sécurité : Si nous sommes déjà à la fin, nous arrêtons l'exécution ou terminons immédiatement.
        if (currentTutorialStep >= TUTORIAL_STEP_END)
        {
            EndTutorial();
            return;
        }

        // 1. Incrémenter l'étape
        currentTutorialStep++;
        Debug.Log($"Tutoriel avancé à l'étape {currentTutorialStep}");

        // 2. Déclencher l'événement pour la nouvelle étape (pour que le TutorialStepManager réagisse)
        TutorialEvents.TutorialStepAdvanced(currentTutorialStep);

        // 3. VÉRIFIER LA FIN IMMÉDIATEMENT après l'incrémentation
        if (currentTutorialStep == TUTORIAL_STEP_END)
        {
            // ⚠️ La fin est atteinte. Déclencher l'événement de fin immédiatement
            // APRES que l'événement de l'étape finale ait eu le temps d'activer son contenu.
            Debug.Log("Dernière étape de contenu atteinte. Déclenchement de la fin.");
            
            // C'est ici que l'appel à EndTutorial doit se produire
            // Note: Si l'étape TUTORIAL_STEP_END (6) est juste une étape de transition/remerciement,
            // vous pouvez appeler EndTutorial() ici.
            EndTutorial(); 
        }
    }

    public void EndTutorial()
    {
        currentTutorialStep = 0;
        TutorialEvents.TutorialEnded(Tutorial_name);
        Debug.Log("Le tutoriel est terminé.");
    }

    // when a dialogue is started
    public void CheckStartDialogue(string dialogueName)
    {
        
    }

    // when a dialogue is finished
    public void CheckEndDialogue(string dialogueName)
    {
        if (dialogueName == "AI_SPACE_DIALOGUE_1")
        {
            Debug.Log("diag ended");
            // step 1
            AdvanceTutorialStep();

            // if (currentTutorialStep == TUTORIAL_STEP_END)
            // {
            //     Debug.Log("Fin du tutoriel atteinte. Appel de EndTutorial.");
            //     EndTutorial();
            // }
        }
    }

    // (Dans GameStateManager.cs)

    public void CheckedFinieshedObjective(string objective_id)
    {
        Debug.Log($"Objectif terminé : {objective_id}");


        if (!isTutorialActive) return;
        // Si l'objectif terminé est l'un des objectifs qui font avancer le tutoriel
        if (objective_id == "Touch_slider" && currentTutorialStep == 2)
        {
            AdvanceTutorialStep();
        }
        if (objective_id == "Touch_move" && currentTutorialStep == 4)
        {
            // On avance immédiatement à l'étape suivante, ce qui lancera la vérification de fin.
            AdvanceTutorialStep();
        }
    }

    // public void CheckWaitingTutorial(string objective_id)
    // {
    //     // tyo start the slider step
    //     if (objective_id.Equals("Touch_slider"))
    //     {
    //         AdvanceTutorialStep();
    //     }
    // }
    // (Dans GameStateManager.cs)

    public void CheckWaitingTutorial(string objective_id)
    {
        // C'est ici qu'on gère la logique de MISE EN PLACE de l'objectif (ex: surligner un bouton).
        // Cette fonction ne devrait PAS faire avancer le tutoriel, car l'objectif n'est pas encore accompli.

        if (!isTutorialActive) return;
        // Vous pouvez laisser un Debug.Log pour la confirmation :
        Debug.Log($"Tutoriel en pause. Objectif attendu : {objective_id}");


        if (objective_id == "Touch_slider" || objective_id == "Touch_move")
        {
            // On avance immédiatement à l'étape suivante, ce qui lancera la vérification de fin.
            AdvanceTutorialStep();
        }
    }

    public void TutorialStarted(string tuto_name)
    {
        Tutorial_name = tuto_name;
    }
    public void TutorialEnded(string tuto_name)
    {
        if (Tutorial_name == tuto_name)
        {
            // adavnce to the visit quest
            QuestManager.Instance.AcceptQuest(QuestManager.Instance.availableQuests[0].questData);
            isTutorialActive = false;
        }
    }
}
