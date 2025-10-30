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


    void OnEnable()
    {
        // ABONNEMENT : On ajoute la méthode à l'événement
        DialogueManager.OnDialogueStart += CheckStartDialogue;
        DialogueManager.OnDialogueEnd += CheckEndDialogue;

        //
        TutorialEvents.OnObjectiveAwaiting += CheckWaitingTutorial;

        TutorialEvents.OnTutorialObjectiveCompleted += CheckedFinieshedObjective;
    }

    void OnDisable()
    {
        // DÉSABONNEMENT : Essentiel pour éviter les fuites de mémoire et les erreurs !
        DialogueManager.OnDialogueStart -= CheckStartDialogue;
        DialogueManager.OnDialogueEnd -= CheckEndDialogue;


        TutorialEvents.OnObjectiveAwaiting -= CheckWaitingTutorial;
        TutorialEvents.OnTutorialObjectiveCompleted -= CheckedFinieshedObjective;
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
    private const int TUTORIAL_STEP_END = 5; 

    public bool IsTutorialActive()
    {
        // Le tutoriel est actif si l'étape actuelle est > 0
        return currentTutorialStep > 0;
    }

    public void AdvanceTutorialStep()
    {
        if (currentTutorialStep > 0 && currentTutorialStep < TUTORIAL_STEP_END)
        {
            currentTutorialStep++;
            TutorialEvents.TutorialStepAdvanced(currentTutorialStep);
            Debug.Log($"Tutoriel avancé à l'étape {currentTutorialStep}");
        }
        else if (currentTutorialStep == TUTORIAL_STEP_END)
        {
            EndTutorial();
        }
    }

    public void EndTutorial()
    {
        currentTutorialStep = 0;
        TutorialEvents.TutorialEnded();
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
            // step 1
            //AdvanceTutorialStep();
            // call for dialogue change -> in teext box
        }
        // else if (dialogueName == "AI_SPACE_DIALOGUE_2")
        // {
        //     // step 2
        //     AdvanceTutorialStep();
        // }
    }

    public void CheckedFinieshedObjective(string objective_id)
    {

        Debug.Log($"Objectif terminé : {objective_id}");
        // top finish the slider step
        if (objective_id == "Touch_slider")
        {
            AdvanceTutorialStep();
        }
        else if (objective_id == "Touch_move")
        {
            AdvanceTutorialStep();
        }
    }

    public void CheckWaitingTutorial(string objective_id)
    {
        // tyo start the slider step
        if (objective_id.Equals("Touch_slider"))
        {
            AdvanceTutorialStep();
        }
    }
}
