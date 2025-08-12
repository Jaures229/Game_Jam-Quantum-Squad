using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            Debug.Log("ouii");
            QuestManager.Instance.NotifyItemCollected("Stone");

            Destroy(other.gameObject);
        }
    }
}
