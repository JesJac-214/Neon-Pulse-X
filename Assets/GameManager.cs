using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsPaused;

    public GameObject pauseMenu;

    void Start()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
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

    public void OnReturnMainMenu()
    {
        IsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartTrack()
    {
        IsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
