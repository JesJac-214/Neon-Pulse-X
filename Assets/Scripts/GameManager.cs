using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool IsPaused;
    private bool pausable = true;

    public GameObject pauseMenu;
    public GameObject winScreen;

    public TMP_Text winnerDeclaration;
    public TMP_Text[] WeaponAmmoHUDs;
    //public TMP_Text[] ItemAmmoHUDs;

    [SerializeField]
    private GameObject[] playerHeartContainers;

    [SerializeField]
    private GameObject[] playerHUDs;

    //public Button resumeButton;
    //public Button restartButton;

    public AudioSource raceMusicSource;
    public AudioSource pauseSource;
    public AudioSource pauseMusicSource;

    public AudioClip pauseSound;
    public AudioClip unpauseSound;
    public AudioClip winSound;

    void Start()
    {
        IsPaused = false;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        foreach (GameObject playerHUD in playerHUDs)
        {
            playerHUD.SetActive(false);
        }
        GameObject[] vehicles;
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            playerHUDs[vehicle.GetComponent<VehicleData>().playerID].SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (pausable)
        {
            IsPaused = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            raceMusicSource.Pause();
            pauseMusicSource.Play();
            pauseMenu.SetActive(true);
            //pauseSource.PlayOneShot(pauseSound, 1f);
            //pauseText.gameObject.SetActive(true);
            //resumeButton.gameObject.SetActive(true);
        }
    }

    public void UnpauseGame()
    {
        IsPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        raceMusicSource.Play();
        pauseMusicSource.Stop();
        pauseMenu.SetActive(false);
        //pauseSource.PlayOneShot(unpauseSound, 1f);
    }

    public void GameWin(int ID)
    {
        raceMusicSource.Stop();
        pausable = false;
        winScreen.SetActive(true);
        winnerDeclaration.text = "Player " + ID + " wins!";
        pauseSource.PlayOneShot(winSound, 1f);
        StartCoroutine(nameof(WinDelay));
    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("VictoryPodium");
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void OnReturnMainMenu()
    {
        IsPaused = false;
        Time.timeScale = 1;
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject vehicle in vehicles)
        {
            Destroy(vehicle.transform.parent.gameObject);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartTrack()
    {
        IsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        if (vehicles.Length > 0)
        {
            foreach (GameObject vehicle in vehicles)
            {
                if (vehicle.GetComponent<VehicleData>().lives < 3)
                {
                    playerHeartContainers[vehicle.GetComponent<VehicleData>().playerID].transform.GetChild(vehicle.GetComponent<VehicleData>().lives).gameObject.SetActive(false);
                }
                //WeaponAmmoHUDs[vehicle.GetComponent<VehicleData>().playerID].text = vehicle.GetComponent<VehicleWeaponItemLogic>().Weapon.ammo.ToString();
                WeaponAmmoHUDs[vehicle.GetComponent<VehicleData>().playerID].text = vehicle.GetComponent<VehicleWeaponItemLogic>().Item.ammo.ToString();
            }
        }
        if (vehicles.Length > 1)
        {
            int deadCount = 0;
            int winnerID = 0;
            foreach (GameObject vehicle in vehicles)
            {
                if (vehicle.GetComponent<VehicleData>().isDead)
                {
                    deadCount++;
                }
                else
                {
                    winnerID = vehicle.GetComponent<VehicleData>().playerID + 1;
                }
                if (deadCount == vehicles.Length - 1)
                {
                    GameWin(winnerID);
                    enabled = false;
                }
            }
        }
    }
}
