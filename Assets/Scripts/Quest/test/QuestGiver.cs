using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    // Référence à la quête que ce PNJ donne.
    // C'est un ScriptableObject, vous devez le glisser-déposer dans l'Inspecteur.
    public Quest questToGive;

    // Référence au QuestManager pour interagir avec le système de quêtes global.
    private QuestManager questManager;

    private void Start()
    {
        // On récupère l'instance du QuestManager au démarrage.
        // On s'assure que le QuestManager est un Singleton et est toujours présent.
        if (QuestManager.Instance != null)
        {
            questManager = QuestManager.Instance;
        }
        else
        {
            Debug.LogError("QuestGiver n'a pas pu trouver une instance de QuestManager. Assurez-vous qu'il est présent dans la scène.");
        }
    }

    // Cette méthode est le point d'entrée de l'interaction avec le PNJ.
    public void Interact()
    {
        if (questManager == null || questToGive == null)
        {
            Debug.LogWarning("Le QuestGiver n'est pas correctement configuré.");
            return;
        }

        // Vérification de l'état de la quête par rapport au joueur.
        // On utilise l'ID de la quête pour vérifier son état dans les listes du QuestManager.
        
        // 1. La quête est-elle déjà complétée ?
        if (questManager.completedQuests.Exists(qs => qs.questData == questToGive))
        {
            Debug.Log($"[PNJ] Vous avez déjà terminé la quête : {questToGive.questName}.");
            return;
        }

        // 2. La quête est-elle active (en cours) ?
        if (questManager.activeQuests.Exists(qs => qs.questData == questToGive))
        {
            // La quête est en cours, on vérifie si elle est prête à être rendue.
            if (questToGive.IsCompleted())
            {
                Debug.Log($"[PNJ] Félicitations ! La quête '{questToGive.questName}' est terminée. Voici votre récompense !");
                questManager.CompleteQuest(questToGive);
            }
            else
            {
                Debug.Log($"[PNJ] La quête '{questToGive.questName}' est en cours. Revenez me voir quand vous aurez terminé.");
            }
            return;
        }

        // 3. La quête est-elle disponible (débloquée, non acceptée) ?
        if (questManager.availableQuests.Exists(qs => qs.questData == questToGive))
        {
            Debug.Log($"[PNJ] Bonjour, j'ai une quête pour vous : '{questToGive.questName}'. Acceptez-vous de m'aider ?");
            questManager.AcceptQuest(questToGive);
        }
        // 4. La quête est-elle verrouillée ?
        else
        {
            Debug.Log($"[PNJ] J'ai peut-être quelque chose pour vous plus tard, mais pas pour le moment.");
        }
    }
}