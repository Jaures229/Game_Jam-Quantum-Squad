using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
public class NitroManagement : MonoBehaviour
{
    [SerializeField] private Image _nitroImage;
    [SerializeField] Sprite[] sprites;
    private float _timeForLoad = 60f;
    private float _timer = 0f;

    private void Start()
    {
        StartCoroutine(LoadNitro());
    }

    private IEnumerator LoadNitro()
    {
        while (_timer < _timeForLoad)
        {
            _timer += Time.deltaTime;
            float progression = _timer / _timeForLoad;
            UpdateNitro(progression);
            yield return null;
        }
        UpdateNitro(1f);
    }

    private void UpdateNitro (float value)
    {
        int index = Mathf.Clamp(Mathf.FloorToInt(value * sprites.Length), 0, sprites.Length - 1);
        _nitroImage.sprite = sprites[index];
    }

    void Update()
    {
        
    }
}
