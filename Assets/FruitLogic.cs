using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class FruitLogic : MonoBehaviour
{
    Animator animator; 

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
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
