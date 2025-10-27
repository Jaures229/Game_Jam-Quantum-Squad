using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ElementFade : MonoBehaviour
{

    public bool playAtStart = true;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Tween currentTween;

    [Header("Réglage d'animation")]
    public float fadeDuration = 0.5f;
    public float scaleDuration = 0.5f;
    public Ease easeType = Ease.OutQuad;

    [Header("Effets optionnels")]
    public bool useScale = true;
    public float startScale = 0.9f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (playAtStart == true)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        KillCurrentTween();

        // Reset de l’état
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (useScale && rectTransform != null)
            rectTransform.localScale = Vector3.one * startScale;

        // Lancer le tween
        Sequence seq = DOTween.Sequence();

        seq.Append(canvasGroup.DOFade(1f, fadeDuration).SetEase(easeType));

        if (useScale && rectTransform != null)
            seq.Join(rectTransform.DOScale(1f, scaleDuration).SetEase(easeType));

        seq.OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });

        currentTween = seq;
    }

    public void FadeOut()
    {
        KillCurrentTween();

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        Sequence seq = DOTween.Sequence();
        seq.Append(canvasGroup.DOFade(0f, fadeDuration).SetEase(easeType));

        if (useScale && rectTransform != null)
            seq.Join(rectTransform.DOScale(startScale, scaleDuration).SetEase(easeType));

        currentTween = seq;
    }

    private void KillCurrentTween()
    {
        if (currentTween != null && currentTween.IsActive())
            currentTween.Kill();
    }
}
