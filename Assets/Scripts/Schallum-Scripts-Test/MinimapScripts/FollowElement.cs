using UnityEngine;

public class FollowElement : MonoBehaviour
{
    public GameObject parent;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(parent.transform.position.x, transform.position.y, parent.transform.position.z);
    }
}
