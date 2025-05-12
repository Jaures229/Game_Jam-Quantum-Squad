using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mars_display_description : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject description_object;
    public GameObject Progression_objet;

    public void DisplayDescription()
    {
        description_object.SetActive(!description_object.activeSelf);
        Progression_objet.SetActive(!Progression_objet.activeSelf);
    }

}
