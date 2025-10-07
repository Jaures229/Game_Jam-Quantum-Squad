using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    // Exemple 1 : Définir la cible en pressant une touche
    public Transform planeteCible;
    void Start()
    {
        StartMissionVersPlanete();
    }

    // Référence à la planète qui bouge (à assigner dans l'Inspector)

    public void StartMissionVersPlanete()
    {
        if (planeteCible != null)
        {
            // Appel au Manager, en lui donnant la référence au Transform de la planète
            GPSManager.Instance.SetNewTarget(planeteCible, planeteCible.gameObject.name);
        }
    }
}
