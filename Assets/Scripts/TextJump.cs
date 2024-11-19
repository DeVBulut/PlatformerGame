using UnityEngine;

public class TextJump : MonoBehaviour
{

    [SerializeField] private float hoverSpeed = 4f; // Speed of the hover effect
    [SerializeField] private float hoverHeight = 0.2f; // Height of the hover effect
    private float originalY;
    void Start()
    {
        originalY = transform.position.y;
    }
    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = originalY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
