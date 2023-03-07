using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class VehicleData : MonoBehaviour
{
	public int playerID = 0;
	public Transform startTransform;
	public int courseProgress = 0;
	public int laps = 0;
	public int lives = 3;
	public bool isDead = false;
	public bool isReady = false;
	public GameManager gameManager;
	public int placement = 0;

	private float ignoreReadyUpTime;

	//private void Start()
	//{
	//	gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
	//}
    private void OnEnable()
    {
		SceneManager.sceneLoaded += OnGameStart;
        ignoreReadyUpTime = Time.time + 0.1f;
    }

    private void OnDisable()
    {
		SceneManager.sceneLoaded -= OnGameStart;
	}
    private void OnGameStart(Scene scene, LoadSceneMode mode)
    {
		if (scene.name == "Real_track 2")
        {
			gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
		}
    }
    public void OnPauseGame()
	{
		//gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
		if (!gameManager.IsPaused)
        {
			gameManager.PauseGame();
        }
		else
        {
			gameManager.UnpauseGame();
        }
	}

	public void IncrementProgress()
	{
		courseProgress++;
	}

	public void DecrementProgress()
	{
		courseProgress--;
	}

	public void OnReadyUp(InputAction.CallbackContext context)
    {
		//if (context.started && Time.time > ignoreReadyUpTime)
		//{
		//	isReady = true;
		//}
    }

    private void OnTriggerStay(Collider other)
    {
		if (other.CompareTag("Ready Zone"))
		{
			isReady = true;
		}
	}

    private void OnTriggerExit(Collider other)
    {
		if (other.CompareTag("Ready Zone"))
        {
			isReady = false;
        }
    }
}
