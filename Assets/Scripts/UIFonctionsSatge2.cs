using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFonctions : MonoBehaviour
{
    [SerializeField] GameObject description;
    [SerializeField] Sprite[] HelfState;
    [SerializeField] private Image _healthBar;

    void Start()
    {
        
    }

    void Update()
    {
        ChangeHelathBar();
    }

    public void PrintMission ()
    {
        if (description.activeInHierarchy == false)
        {
            description.SetActive(true);
        }
        else
        {
            description.SetActive(false);
        }

    }

    private void ChangeHelathBar ()
    {
        //_healthBar.sprite = HelfState[9 - PlayerControler.instance._healthBar];
    }
}
