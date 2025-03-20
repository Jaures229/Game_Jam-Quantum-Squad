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

        _nitroAnimator.SetBool("Unload", true);
        _nitroAnimator.SetBool("Load", false);

        yield return new WaitForSeconds(_boostDuration);

        yield return new WaitForSeconds(_cooldownBeforeReload);

        _boostButton.interactable = true;
        _isBoosting = false;
        StartCoroutine(LoadNitro());
    }

    private IEnumerator LoadNitro()
    {
        _isCharging = true;

        _boostButton.interactable = false;

        _nitroAnimator.SetBool("Unload", false);
        _nitroAnimator.SetBool("Load", true);

        yield return new WaitForSeconds(_timeForLoad);
        _boostButton.interactable = true;
        _isCharging = false;
    }


}
