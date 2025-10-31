using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // <<< NÉCESSAIRE POUR LES TRANSITIONS FADE >>>
using TMPro;
public class QuestShow : MonoBehaviour
{
    [Header("Bouton et Sprites")]
    [Tooltip("Le Sprite à afficher lorsque la progression est visible (icône de masquage).")]
    public Sprite hide_sprite; // <<< CHANGÉ DE Image à Sprite >>>
    
    [Tooltip("Le Sprite à afficher lorsque la progression est masquée (icône de visibilité).")]
    public Sprite see_sprite;  // <<< CHANGÉ DE Image à Sprite >>>

    [Tooltip("Le bouton qui déclenche l'action.")]
    public Button button;

    [Header("Panneau de Progression de Quête")]
    public GameObject QuestProgression;
    public GameObject QuestNameText;


    [Header("Paramètres de l'Animation")]
    public float fadeDuration = 0.3f;
    
    private bool isPanelVisible = false;
    private Image buttonIconImage; // Référence au composant Image du bouton

    void Start()
    {
        // 1. S'assurer que le panneau de progression a un CanvasGroup pour les effets de Fade
        if (QuestProgression.GetComponent<CanvasGroup>() == null)
        {
            QuestProgression.AddComponent<CanvasGroup>();
        }

        // 2. Assurez-vous que le panneau est initialement masqué et que le bouton est configuré
        QuestProgression.GetComponent<CanvasGroup>().alpha = 0f;
        QuestProgression.SetActive(false);
        SetButtonIcon(false); // Afficher l'icône 'masquer' (car l'objet est masqué)


        // **NOUVEAU :** Récupérer le composant Image du bouton une seule fois au démarrage
        buttonIconImage = button.GetComponent<Image>();

        if (buttonIconImage == null)
        {
            Debug.LogError("Le Bouton doit avoir un composant Image pour afficher l'icône.");
            return;
        }

        // 1. S'assurer que le panneau de progression a un CanvasGroup
        if (QuestProgression.GetComponent<CanvasGroup>() == null)
        {
            QuestProgression.AddComponent<CanvasGroup>();
        }

        // 2. Assurez-vous que le panneau est initialement masqué
        QuestProgression.GetComponent<CanvasGroup>().alpha = 0f;
        QuestProgression.SetActive(false);
        
        SetButtonIcon(false); // Initialiser l'icône sur 'voir'

        // 3. Ajouter l'écouteur de clic au bouton
        if (button != null)
        {
            button.onClick.AddListener(ToggleQuestProgression);
        }
    }

    /// <summary>
    /// Bascule l'état du panneau de progression de quête.
    /// </summary>
    public void ToggleQuestProgression()
    {
        // Inverser l'état
        isPanelVisible = !isPanelVisible;

        if (isPanelVisible)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
    }

    /// <summary>
    /// Affiche le panneau avec un effet de Fade In.
    /// </summary>
    private void ShowPanel()
    {
        // 1. Activer l'objet (pour que le CanvasGroup fonctionne)
        QuestProgression.SetActive(true);

        // Récupérer le CanvasGroup pour l'animation
        CanvasGroup canvasGroup = QuestProgression.GetComponent<CanvasGroup>();
        
        // 2. Animer le Fade In (alpha de 0 à 1)
        canvasGroup.DOFade(1f, fadeDuration)
            .SetEase(Ease.OutSine); // Ajoute une courbe d'accélération/décélération
        
        // 3. Mettre à jour l'icône du bouton
        SetButtonIcon(true);
    }

    /// <summary>
    /// Masque le panneau avec un effet de Fade Out.
    /// </summary>
    private void HidePanel()
    {
        CanvasGroup canvasGroup = QuestProgression.GetComponent<CanvasGroup>();

        // 1. Animer le Fade Out (alpha de 1 à 0)
        canvasGroup.DOFade(0f, fadeDuration)
            .SetEase(Ease.InSine)
            // 2. Quand l'animation est terminée, désactiver l'objet pour optimiser les Raycasts
            .OnComplete(() => QuestProgression.SetActive(false));

        // 3. Mettre à jour l'icône du bouton
        SetButtonIcon(false);
    }

    /// <summary>
    /// Définit l'image du bouton en utilisant directement les assets Sprite.
    /// </summary>
    private void SetButtonIcon(bool visible)
    {
        if (buttonIconImage == null) return;
        
        if (visible)
        {
            // Si le panneau est VISIBLE, on veut l'icône pour le MASQUER (hide_sprite)
            buttonIconImage.sprite = hide_sprite;
        }
        else
        {
            // Si le panneau est MASQUÉ, on veut l'icône pour l'AFFICHER (see_sprite)
            buttonIconImage.sprite = see_sprite;
        }
    }
}
