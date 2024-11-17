using UnityEngine;

public class SawController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;

    void Start()
    {
        // Set the initial target to the first point
        target = pointB;
    }

    void Update()
    {
        // Move the object toward the target position
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // If the object reaches the target, switch to the other position
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
