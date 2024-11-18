using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelInteractionController : MonoBehaviour
{
    private PlayerController playerController;
    private int fruitCount = 0;
    private int localFruitCount = 0;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI totalScoreInLevel; //Change this mckay to what you decided on the max amount of fruits per level. 

    void Start()
    {
        totalScoreInLevel.text = "3";
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerController = GetComponent<PlayerController>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        localFruitCount = 0;
    }

    void OnDestroy()
    {
        // Unregister the event when the GameObject is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == VariableList.CheckPointTag)
        {
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Checkpoint Reached!");
            other.gameObject.GetComponent<Animator>().SetTrigger("Reach");
            StartCoroutine(InitiateDeSpawn());
            StartCoroutine(InitiateSceneChange());
        }
        else if(other.tag == VariableList.FruitTag)
        {
            Debug.Log(other.gameObject.name + " has been collected!");
            fruitCount += 1; 
            localFruitCount += 1;
            UpdateUI();
        }
        else if(other.tag == VariableList.SawTag)
        {
            Debug.Log(other.gameObject.name + " has hit you!");
            playerController.Die();
        }
    }

    private IEnumerator InitiateSceneChange()
    {
        yield return new WaitForSeconds(3f);
        LoadNextLevel();
    }

    private IEnumerator InitiateDeSpawn()
    {
        yield return new WaitForSeconds(1f);
        playerController.DeSpawn();
    }

    private void UpdateUI()
    {
        currentScore.text = localFruitCount.ToString();
    }
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels to load! This is the last level.");
        }
    }
}
