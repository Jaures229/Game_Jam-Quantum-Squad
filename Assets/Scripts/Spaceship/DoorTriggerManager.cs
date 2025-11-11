using UnityEngine;

// Ce script est attaché à la zone de déclenchement (Trigger Zone).
public class DoorTriggerManager : MonoBehaviour
{
    [Header("Portes à Contrôler")]
    // Tableau des scripts DoorController de TOUTES les portes à ouvrir
    public DoorController[] controlledDoors;

    [Header("Configuration du Joueur")]
    public string playerTag = "Player";

    // Assurez-vous que ce GameObject a un Collider avec 'Is Trigger' coché.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Joueur entré. Ouverture de " + controlledDoors.Length + " porte(s).");
            foreach (DoorController door in controlledDoors)
            {
                if (door != null)
                {
                    door.Open(); // Appelle la fonction Open sur chaque porte
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Joueur sorti. Fermeture de " + controlledDoors.Length + " porte(s).");
            foreach (DoorController door in controlledDoors)
            {
                if (door != null)
                {
                    door.Close(); // Appelle la fonction Close sur chaque porte
                }
            }
        }
    }
}
