using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerPrefsEditor : EditorWindow
{
    [MenuItem("Tools/Reset/ Intro PlayerPrefs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerPrefsEditor));

    }

    void OnGUI()
    {
        GUILayout.Label("Reset Intro PlayerPrefs", EditorStyles.boldLabel);
        if (GUILayout.Button("Reset Intro PlayerPrefs"))
        {
            PlayerPrefs.SetInt("_hasSeeIntro", 0);
            PlayerPrefs.Save();
            Debug.Log("_hasSeeIntro = " + PlayerPrefs.GetInt("_hasSeeIntro"));
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
