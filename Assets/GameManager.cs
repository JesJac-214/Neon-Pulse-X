using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsPaused;

    public GameObject pauseMenu;

    void Start()
    {
        IsPaused = false;
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        IsPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
