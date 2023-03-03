using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class VehicleData : MonoBehaviour
{
	public int playerID = 0;
	public Vector3 startPos;
	public int courseProgress = 0;
	public int laps = 0;
	public int lives = 3;
	public bool isDead = false;
	public bool isReady = false;
	public GameManager gameManager;

	//private void Start()
	//{
	//	gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
	//}
    private void OnEnable()
    {
		SceneManager.sceneLoaded += OnGameStart;
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

	public void OnReadyUp()
    {
		isReady = true;
    }
}
