using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField]
    private int minPlayers = 2;

    [SerializeField]
    private GameObject[] ReadyTexts;

    [SerializeField]
    private TMP_Text JoinPrompt;

    [SerializeField]
    private TMP_Text[] ItemHUDs;

    [SerializeField]
    private TMP_Text ReadyAmount;

    private int readyPlayers = 0;

    GameObject[] vehicles;

    private bool countdownRunning = false;

    public AudioSource countdownChime;

    public GameObject pauseMenu;
    public Button resetButton;

    void Update()
    {
        readyPlayers = 0;
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            VehicleData vehicleData = vehicle.GetComponent<VehicleData>();
            ItemHUDs[vehicleData.playerID].text = (vehicle.GetComponent<VehicleWeaponItemLogic>().Item.ammo).ToString();
            ReadyTexts[vehicleData.playerID].SetActive(vehicleData.isReady);
        }
        foreach (GameObject vehicle in vehicles)
        {
            if (vehicle.GetComponent<VehicleData>().isReady)
            {
                readyPlayers++;
            }
        }
        if (vehicles.Length >= minPlayers)
        {
            if (readyPlayers == vehicles.Length)
            {
                if (!countdownRunning)
                {
                    StartCoroutine(nameof(DelayedJoin));
                    countdownRunning = true;
                }
            }
            else
            {
                StopCoroutine(nameof(DelayedJoin));
                countdownRunning = false;
                JoinPrompt.text = "";
            }
        }
        if (vehicles.Length > 0)
        {
            ReadyAmount.text = "(" + readyPlayers + "/" + vehicles.Length + ") Ready!";
            if (readyPlayers == vehicles.Length && readyPlayers < minPlayers)
            {
                ReadyAmount.text = minPlayers + " Players Minimum!";
            }
        }
        if (vehicles.Length == 0)
        {
            JoinPrompt.text = "Press Any Button to Join";
            ReadyAmount.text = "";
            StopCoroutine(nameof(DelayedJoin));
            foreach (GameObject readyText in ReadyTexts)
            {
                readyText.SetActive(false);
            }
            foreach (TMP_Text ammo in ItemHUDs)
            {
                ammo.text = "";
            }
        }
    }

    IEnumerator DelayedJoin()
    {
        for (int i = 3; i > 0; i--)
        {
            JoinPrompt.text = "Loading track in " + i.ToString();
            countdownChime.Play();
            yield return new WaitForSeconds(1);
        }
        foreach (GameObject vehicle in vehicles)
        {
            DontDestroyOnLoad(vehicle.transform.parent.gameObject);
            vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            vehicle.GetComponent<VehicleWeaponItemLogic>().Item = new EquipmentBase();
        }
        SceneManager.LoadScene("Real_track 2");
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        resetButton.Select();
    }

    public void ResetPlayerJoin()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            Destroy(vehicle);
        }
        GameObject.FindWithTag("Respawn").GetComponent<playerspawnmanager>().index = 0;
        GameObject.FindWithTag("Respawn").GetComponent<playerspawnmanager>().manager.playerPrefab = GameObject.FindWithTag("Respawn").GetComponent<playerspawnmanager>().vehicles[0];
        
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClearJoinPrompt()
    {
        JoinPrompt.text = "";
    }
}
