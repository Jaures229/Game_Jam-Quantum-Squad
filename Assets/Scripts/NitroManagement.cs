using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
public class NitroManagement : MonoBehaviour
{
    [SerializeField] private Animator _nitroAnimator;
    [SerializeField] private Button _boostButton;
    [SerializeField] private float _timeForLoad = 20f;
    [SerializeField] private float _boostDuration = 9f;
    [SerializeField] private float _cooldownBeforeReload = 25f;
    private bool _isBoosting = false;
    private bool _isCharging = false;

    private void Start()
    {
        StartCoroutine(LoadNitro());
    }

    public void ActivateBoost()
    {
        if (!_isBoosting && !_isCharging)
        {
            Debug.Log("Debut du boost\n");
            StartCoroutine(UseNitro());
        }
    }

    private IEnumerator UseNitro()
    {
        _isBoosting = true;
        _boostButton.interactable = false;

        // Déclenche l'animation de décharge
        _nitroAnimator.SetBool("Unload", true);
        _nitroAnimator.SetBool("Load", false);

        yield return new WaitForSeconds(_boostDuration);

        // Attends un peu avant de pouvoir recharger, avec le cooldown
        yield return new WaitForSeconds(_cooldownBeforeReload);

        // Réactive le bouton pour le boost et démarre le chargement
        _boostButton.interactable = true;
        _isBoosting = false;
        StartCoroutine(LoadNitro());
    }

    private IEnumerator LoadNitro()
    {
        _isCharging = true;

        _boostButton.interactable = false;

        // Déclenche l'animation de recharge
        _nitroAnimator.SetBool("Unload", false);
        _nitroAnimator.SetBool("Load", true);

        yield return new WaitForSeconds(_timeForLoad);

        // Une fois la recharge terminée, réactive le bouton
        _boostButton.interactable = true;
        _isCharging = false;
        //Debug.Log("Rechargement terminé");
    }


}
