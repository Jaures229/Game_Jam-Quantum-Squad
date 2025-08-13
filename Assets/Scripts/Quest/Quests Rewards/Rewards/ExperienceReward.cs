using UnityEngine;

[CreateAssetMenu(fileName = "NewExperienceReward", menuName = "Quests/Rewards/Experience")]
public class ExperienceReward : QuestReward
{
    // La quantité d'expérience que le joueur recevra
    public int experienceAmount;

    // Cette méthode est appelée par le QuestManager pour appliquer la récompense.
    public override void ApplyReward()
    {
        // On vérifie d'abord si le système de statistiques du joueur existe.
        // if (PlayerStats.Instance != null)
        // {
        //     // On appelle une méthode sur le joueur pour lui ajouter l'expérience.
        //     PlayerStats.Instance.AddExperience(experienceAmount);
        Debug.Log($"Récompense d'expérience appliquée : +{experienceAmount} XP.");
        // }
        // else
        // {
        //     Debug.LogWarning("PlayerStats.Instance non trouvé. Impossible d'appliquer la récompense d'expérience.");
        // }
    }
}
