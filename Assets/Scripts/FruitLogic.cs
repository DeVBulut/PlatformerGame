using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class FruitLogic : MonoBehaviour
{
    Animator animator; 
    private float hoverSpeed = 2f; // Speed of the hover effect
    private float hoverHeight = 0.2f; // Height of the hover effect
    private float originalY;
    public bool isCollected = false;

    private void Start() {
        animator = GetComponent<Animator>();
        originalY = transform.position.y;
    }

    private void FixedUpdate()
    {
        // Calculate the new Y position using a sine wave
        float newY = originalY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Eaten");
            StartCoroutine(InitiateEatSequence());
        }
    }

    private IEnumerator InitiateEatSequence()
    { 
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
       
    }
}
