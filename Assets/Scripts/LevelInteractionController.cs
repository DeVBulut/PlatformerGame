using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelInteractionController : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == VariableList.CheckPointTag)
        {
            Debug.Log("Checkpoint Reached!");
            other.gameObject.GetComponent<Animator>().SetTrigger("Reach"); 
        }
        else if(other.tag == VariableList.FruitTag)
        {
            Debug.Log(other.gameObject.name + " has been collected!");
            Destroy(other.gameObject);
        }
        else if(other.tag == VariableList.SawTag)
        {
            Debug.Log(other.gameObject.name + " has hit you!");
            playerController.Die();
        }
    }
}
