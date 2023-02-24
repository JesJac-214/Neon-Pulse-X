using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleData : MonoBehaviour
{
	public int playerID = 0;
	public Vector3 startPos;
	public int courseProgress = 0;
	public int laps = 0;
	public int lives = 3;
	public void OnQuitGame()
	{
		Application.Quit();
	}

	public void IncrementProgress()
	{
		courseProgress++;
	}

	public void DecrementProgress()
	{
		courseProgress--;
	}
}
