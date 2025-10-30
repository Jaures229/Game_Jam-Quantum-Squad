using UnityEngine;

// Permet de créer facilement l'asset dans le menu d'Unity
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class Dialogue : ScriptableObject
{
    // Le titre de la conversation (pour l'organisation dans le projet)
    public string dialogueTitle = "New Dialogue";

    // Un tableau (liste) de toutes les lignes dans l'ordre de la conversation.
    public DialogueLine[] lines;
}
