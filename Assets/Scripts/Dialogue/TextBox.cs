using UnityEngine;

public class TextBox : MonoBehaviour
{
    public Dialogue dialogueAsset;
    public DialogueManager dialogueManager;
    public Dialogue NextdialogueAsset;
    void OnEnable()
    {
        TutorialEvents.OnTutorialStarted += CheckTutoId;
        DialogueManager.OnDialogueEnd += DialogueChanged;
    }
    void OnDisable()
    {
        TutorialEvents.OnTutorialStarted -= CheckTutoId;
        DialogueManager.OnDialogueEnd -= DialogueChanged;
    }

    public void Interact()
    {
        if (dialogueAsset != null)
        {
            //Debug.Log("interact");
            dialogueManager.StartDialogue(dialogueAsset);
        }
    }

    public void CheckTutoId(string TutoId)
    {
        if (TutoId == "Tutorial_Intro")
        {
            //Debug.Log("oui");
            Interact();
        }
    }

    public void ChangeDialogue(Dialogue newDialogue)
    {
        dialogueAsset = newDialogue;
        Interact();
    }
    public void DialogueChanged(string newDialogue)
    {
        // if (newDialogue == "AI_SPACE_DIALOGUE_1")
        // {
        //     ChangeDialogue(NextdialogueAsset);
        // }
        // else if (newDialogue == "AI_SPACE_DIALOGUE_2")
        // {
        //     //ChangeDialogue(dialogueAsset);
        // }
    }
}
