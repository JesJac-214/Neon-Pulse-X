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
	public bool isShielded = false;

	public AudioSource engineSource;

	public Transform lastHitCheckpointTransform;

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

	private void Update()
	{
		if (GetComponent<VehicleDrivingAimingLogic>().canAccel)
		{
			engineSource.pitch = 0.4f + GetComponent<Rigidbody>().velocity.magnitude/GetComponent<VehicleDrivingAimingLogic>().maxSpeed;
		}
		else
		{
			engineSource.pitch = 0;
		}
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
			lastHitCheckpointTransform = transform;
		}
    }
    public void OnPauseGame(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			if (SceneManager.GetActiveScene().name == "MainMenu")
			{
				GameObject.FindWithTag("Game Manager").GetComponent<PlayerJoinManager>().PauseGame();
			}
			else if (!gameManager.IsPaused)
			{
				gameManager.PauseGame();
			}
			else if (gameManager.IsPaused)
			{
				gameManager.UnpauseGame();
			}
		}


		// if (context.started && SceneManager.GetActiveScene().name == "MainMenu")
		// {
		// 	Debug.Log("PauseGame");
		// }
		// //gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
		// if (context.started && !gameManager.IsPaused)
        // {
		// 	gameManager.PauseGame();
        // }
		// else if (context.started && gameManager.IsPaused)
        // {
		// 	gameManager.UnpauseGame();
        // }
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
