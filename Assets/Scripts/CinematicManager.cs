using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;  
    [SerializeField] private RawImage displayImage;
    [SerializeField] private List<GameObject> _next;
    [SerializeField] private string _name = null;
    [SerializeField] private NitroManagement nitroManagement;

    private void Start()
    {
        Disable();
        StartCoroutine(PlayCinematic());
    }

    private void Disable()
    {
        if (_next != null)
        {
            foreach (GameObject go in _next)
            {
                go.SetActive(false);
            }
        }
    }

    private void Enable()
    {
        if (_next != null)
        {
            foreach (GameObject go in _next)
            {
                go.SetActive(true);
            }
        }
    }

    private IEnumerator PlayCinematic()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        displayImage.texture = videoPlayer.texture;
        videoPlayer.Play();

        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        Debug.Log("Cinematic terminée !");
        EndCinematic();
    }

    private void EndCinematic()
    {
        if (!string.IsNullOrEmpty(_name))
        {
            SceneManager.LoadScene(_name);
        } else
        {
            Enable();
            if (nitroManagement != null)
            {
                StartCoroutine(EnableAndStartNitro());
            }
        }
        gameObject.SetActive(false);
    }
    private IEnumerator EnableAndStartNitro()
    {
        yield return new WaitForSeconds(2.0f);
        if (nitroManagement != null)
        {
            StartCoroutine(nitroManagement.LoadNitro());
        }
    }
}
