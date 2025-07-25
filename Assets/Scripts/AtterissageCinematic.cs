using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class AtterissageCinematic : MonoBehaviour
{
    [SerializeField] private List<GameObject> _next;
    //public Quest MarsFirstQuest;


    private void Enable()
    {
        foreach (GameObject go in _next)
        {
            go.SetActive(true);
        }
    }

    private void Start()
    {
        /*if (playButton != null)
        {
            playButton.onClick.AddListener(PlayNewCinematic);
        }*/
    }

    public void PlayNewCinematic()
    {
        // if (QuestManager.Instance.currentQuest.Equals(MarsFirstQuest)) {
        //     QuestEvents.OnPlanetVisited?.Invoke("Mars");
        // }
        Enable();
    }
}
