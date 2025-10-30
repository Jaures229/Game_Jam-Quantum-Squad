using UnityEngine;


// 1. DÉFINITION DE L'ENUM pour les types d'avancement
public enum AdvanceMode
{
    // Le joueur avance la ligne en appuyant sur l'écran/bouton (mode par défaut)
    UserInput,
    // Le dialogue attend qu'un événement externe se produise pour avancer (tâche accomplie, timer, etc.)
    WaitForEvent
}

[System.Serializable]
public class DialogueLine
{
    // Nom du personnage qui parle.
    public string characterName;

    // 2. NOUVEAU CHAMP : Détermine comment le jeu doit avancer après cette ligne.
    public AdvanceMode advancement = AdvanceMode.UserInput;
    public string objective_id = "None";
    // Le texte complet à afficher.
    [TextArea(3, 10)] // Rend le champ de texte plus grand dans l'Inspecteur
    public string text;

    // (Optionnel) Référence à l'image du portrait.
    public Sprite portrait;

    // (Optionnel) Un clip audio de la voix si vous en avez.
    public AudioClip voiceClip;
}
