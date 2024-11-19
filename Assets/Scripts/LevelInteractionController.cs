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
    private bool checkPointReached;
    private GameObject currentCheckpoint;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI totalScoreInLevel; //Change this mckay to what you decided on the max amount of fruits per level. 
    public AudioSource audioSource;
    public AudioClip fruitEatenSound;
    public AudioClip spawnSound;
    public AudioClip flagSound; 
    public AudioClip winSound;
    public GameObject canvas;
    public TextMeshProUGUI totalScoreAtEnd; 



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
        UpdateUI();
        checkPointReached = false; 
    }

    void OnDestroy()
    {
        // Unregister the event when the GameObject is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Update()
    {
        if(checkPointReached == true)
        {
            if(Input.GetKey(KeyCode.G))
            {
                checkPointReached = false; 
                audioSource.clip = flagSound; 
                audioSource.Play();
                StartCoroutine(InitiateDeSpawn());
                StartCoroutine(InitiateSceneChange());
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == VariableList.CheckPointTag)
        {
            currentCheckpoint =  other.gameObject;
            checkPointReached = true;
            InitiateCheckPointFunctionality(currentCheckpoint);
        }
        else if (other.tag == VariableList.FruitTag)
        {
            FruitLogic fruitComponent = other.gameObject.GetComponent<FruitLogic>();

            if (fruitComponent != null && !fruitComponent.isCollected)
            {
                fruitComponent.isCollected = true;
                //other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                Debug.Log(other.gameObject.name + " has been collected!");
                fruitCount += 1;
                localFruitCount += 1;
                UpdateUI();

                audioSource.clip = fruitEatenSound;
                audioSource.Play();
            }
        }
        else if (other.tag == VariableList.SawTag)
        {
            Debug.Log(other.gameObject.name + " has hit you!");
            if (playerController.stateMachine.CurrentPlayerState != playerController.deathState) { audioSource.clip = spawnSound;  audioSource.Play(); }
            playerController.Die();
        }
        else if (other.tag == VariableList.Border)
        {
            Debug.Log(other.gameObject.name + " - hit the border!");
            if (playerController.stateMachine.CurrentPlayerState != playerController.deathState) { audioSource.clip = spawnSound;  audioSource.Play(); }
            playerController.Die();
        }
    
   }

    private void InitiateCheckPointFunctionality(GameObject other)
    {
        if(other.GetComponent<Collider2D>().enabled == true){ audioSource.clip = winSound;  audioSource.Play(); };
        other.GetComponent<Collider2D>().enabled = false;
        Debug.Log("Checkpoint Reached!");

        other.GetComponent<Animator>().SetTrigger("Reach");

        if (other.transform.GetChild(0) != null) { other.transform.GetChild(0).gameObject.SetActive(true); } else { Debug.LogWarning("UI Text Missing!"); }

    }

    private IEnumerator InitiateSceneChange()
    {
        yield return new WaitForSeconds(3f);
        LoadNextLevel();
    }

    private IEnumerator InitiateDeSpawn()
    {
        yield return new WaitForSeconds(1f);
        audioSource.clip = spawnSound; 
        playerController.DeSpawn();
    }

    private void UpdateUI()
    {
        currentScore.text = localFruitCount.ToString();
    }
    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().name == "FinalScene"){ InitiateFinalSquence(); return;  }
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

    public void InitiateFinalSquence()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        totalScoreAtEnd.text = fruitCount.ToString(); 
        currentCheckpoint.transform.GetChild(0).gameObject.SetActive(false);
    }
}
