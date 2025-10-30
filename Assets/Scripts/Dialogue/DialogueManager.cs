using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    // Référence à votre composant TextMeshPro (n'oubliez pas de l'assigner dans l'Inspecteur)
    [SerializeField] private TextMeshProUGUI dialogueText;

    // Vitesse d'affichage (caractères par seconde)
    [SerializeField] private float typingSpeed = 50f;

    // La file d'attente pour stocker toutes les lignes de dialogue
    //private Queue<string> sentences;
    private Queue<DialogueLine> dialogueLines;
    // Le Coroutine pour l'effet de machine à écrire
    private Coroutine typingCoroutine;

    // Variable pour savoir si l'affichage est terminé pour la ligne actuelle
    private bool isTypingFinished = false;


    // --- NOUVEAUX ÉVÉNEMENTS C# NATIFS ---

    // Event déclenché au début du dialogue, transmet le nom du dialogue (string)
    public static event Action<string> OnDialogueStart;

    // Event déclenché à la fin du dialogue, transmet le nom du dialogue (string)
    public static event Action<string> OnDialogueEnd;

    // Variable pour stocker la ligne actuelle
    private DialogueLine currentLine; 

    // Variable d'état pour le mode d'avancement
    private bool isWaitingForObjective = false;
    private Dialogue currentDialogue;

    // ---------------------------

    void Awake() 
    {
        dialogueLines = new Queue<DialogueLine>();
        // ...
    }
    void Start()
    {
        //sentences = new Queue<string>();
        // Assurez-vous que la boîte de dialogue est initialement cachée
        //gameObject.SetActive(false); 
    }

    void OnEnable()
    {
        // Abonnement pour l'avancement des dialogues standard (UserInput)
        DialogueUIManager.OnScreenTouched += HandleInput;
        TutorialEvents.OnTutorialObjectiveCompleted += ObjectiveCompleted;
        // ... Autres abonnements
    }

    void OnDisable()
    {
        // Désabonnement
        DialogueUIManager.OnScreenTouched -= HandleInput;
        TutorialEvents.OnTutorialObjectiveCompleted -= ObjectiveCompleted;
        // ... Autres désabonnements
    }

    /// <summary>
    /// Commence un nouveau dialogue en utilisant les données du ScriptableObject.
    /// </summary>
    public void StartDialogue(Dialogue dialogueData)
    {
        // ... (Déclenchement des Events) ...
        currentDialogue = dialogueData;
        OnDialogueStart?.Invoke(dialogueData.dialogueTitle);
        dialogueLines.Clear();
        foreach (DialogueLine line in dialogueData.lines)
        {
            dialogueLines.Enqueue(line); // On ajoute l'objet complet
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Affiche la prochaine ligne de dialogue.
    /// Cette fonction NE DOIT ÊTRE APPELÉE que lorsque le texte actuel est entièrement affiché.
    /// </summary>
    public void DisplayNextSentence()
    {
        // RETIRER la première condition `if (!isTypingFinished)` : 
        // On suppose que l'appelant (HandleInput) a déjà géré l'affichage instantané.

        if (isWaitingForObjective)
        {
            // IMPORTANT : Si nous attendons un objectif, on bloque l'avancement.
            Debug.Log("Le dialogue est en pause. En attente de l'objectif...");
            return;
        }
       // Debug.Log(dialogueLines.Count);
        // Passage à la ligne suivante
        if (dialogueLines.Count == 0)
        {
            //Debug.Log("oui_ended");
            EndDialogue();
            return;
        }

        currentLine = dialogueLines.Dequeue();
        
        // Vérification du mode d'avancement de la ligne
        if (currentLine.advancement == AdvanceMode.WaitForEvent)
        {
            isWaitingForObjective = true; // Active l'état d'attente
            Debug.Log($"Objectif à accomplir : {currentLine.text}");
            // 🚨 IMPORTANT : Déclenchez ici l'événement/le système qui gère le tutoriel/l'objectif
            TutorialEvents.ObjectiveAwaiting(currentLine.objective_id); 
        }
        else // AdvanceMode.UserInput
        {
            isWaitingForObjective = false; // Désactive l'état d'attente (mode normal)
        }

        // Lance l'effet machine à écrire avec le texte de la ligne
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.text));
    }

    // Fonction à appeler quand le joueur interagit (clic, touche Espace, etc.)
    public void HandleInput()
    {
        if (isWaitingForObjective)
        {
            // L'entrée du joueur ne fait rien si nous sommes en mode 'WaitForEvent'
            Debug.Log("Saisie ignorée, accomplissez l'objectif d'abord.");
            return;
        }

        if (!isTypingFinished)
        {
            // Affiche le reste du texte immédiatement
            isTypingFinished = true;
        }
        else
        {
            // Le texte est fini, on passe à la ligne suivante (si le mode n'était pas WaitForEvent)
            DisplayNextSentence();
        }
    }


    /// <summary>
    /// Appelé par un système externe lorsque l'objectif est accompli.
    /// </summary>
    public void ObjectiveCompleted(string objective_id)
    {
        Debug.Log("DIag on");
        if (objective_id != currentLine.objective_id) return; // Ignore si l'objectif n'est pas celui
        if (!isWaitingForObjective) return; // Ignore si le dialogue n'attend pas d'objectif

        Debug.Log("Objectif accompli ! Le dialogue peut continuer.");

        // Réinitialise l'état et force l'avancement à la prochaine ligne
        isWaitingForObjective = false;
        DisplayNextSentence();
    }

    /// <summary>
    /// La Coroutine qui gère l'effet de machine à écrire.
    /// </summary>
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // Assurez-vous que le champ est vide au départ
        isTypingFinished = false;

        // Calcule le délai entre chaque caractère
        float delay = 1f / typingSpeed;

        for (int i = 0; i < sentence.Length; i++)
        {
            if (isTypingFinished)
            {
                // Si l'affichage instantané est demandé (HandleInput), on affiche tout
                dialogueText.text = sentence;
                break; // Sort de la boucle
            }

            // Ajoute un caractère au texte
            dialogueText.text += sentence[i];

            // Attend le temps calculé (délai)
            yield return new WaitForSeconds(delay);
        }

        // S'assurer que le drapeau est à jour après la fin de la boucle
        isTypingFinished = true;
        typingCoroutine = null;
    }

    /// <summary>
    /// Fin du dialogue (masquer l'UI, reprendre le jeu, etc.).
    /// </summary>
    void EndDialogue()
    {
        if (currentDialogue != null)
        {
            // 2. DÉCLENCHER L'EVENT DE FIN
            OnDialogueEnd?.Invoke(currentDialogue.dialogueTitle);
            Debug.Log($"Event: Dialogue Fini: {currentDialogue.dialogueTitle}");
        }

        currentDialogue = null;
    }
    

}