using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Station : MonoBehaviour
{
    [SerializeField] GameObject _lobby;
    void Start()
    {
        _lobby.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _lobby.SetActive(true);
        } else
        {
            _lobby.SetActive(false);
        }
    }

    public void GotoLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
