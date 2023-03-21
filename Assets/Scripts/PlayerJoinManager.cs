using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField]
    private int minPlayers = 2;

    [SerializeField]
    private GameObject AIVehicle;

    [SerializeField]
    private GameObject[] ReadyTexts;

    [SerializeField]
    private TMP_Text JoinPrompt;

    [SerializeField]
    private TMP_Text[] ItemHUDs;

    [SerializeField]
    private TMP_Text ReadyAmount;

    public GameObject[] PlayerHUDs;

    public TMP_Text playerMessage;

    private int readyPlayers = 0;

    GameObject[] vehicles;

    private bool countdownRunning = false;

    public AudioSource countdownChime;

    public GameObject pauseMenu;
    public Button resetButton;

    public GameObject[] prompts;

    public GameObject[] PlayerIconContainers;
    List<string> iconOrder = new List<string>() { "CannonBall", "EMP", "HackingDevice", "IceBeam", "SoundWave", "Mine", "Shield", "SpeedBoost", "Wall" };

    GameObject spawnManager;

    void Start()
    {
        spawnManager = GameObject.FindWithTag("Respawn");
    }

    void Update()
    {
        readyPlayers = 0;
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            VehicleData vehicleData = vehicle.GetComponent<VehicleData>();
            
            if (vehicle.GetComponent<VehicleWeaponItemLogic>().Item.ammo > 0)
            {
                ItemHUDs[vehicleData.playerID].text = (vehicle.GetComponent<VehicleWeaponItemLogic>().Item.ammo).ToString();
            }
            else
            {
                ItemHUDs[vehicleData.playerID].text = "";
            }
            ReadyTexts[vehicleData.playerID].SetActive(vehicleData.isReady);
            PlayerHUDs[vehicleData.playerID].SetActive(true);
            if (vehicle.GetComponent<VehicleWeaponItemLogic>().Item.ammo > 0)
            {
                PlayerIconContainers[vehicle.GetComponent<VehicleData>().playerID].transform.GetChild(iconOrder.IndexOf(vehicle.GetComponent<VehicleWeaponItemLogic>().Item.weaponName)).gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < iconOrder.Count; i++)
                {
                    PlayerIconContainers[vehicle.GetComponent<VehicleData>().playerID].transform.GetChild(i).gameObject.SetActive(false);
                }
            }
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
            foreach (GameObject HUD in PlayerHUDs)
            {
                HUD.SetActive(false);
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

    List<GameObject> AIVehicles;
    public void AddAIPlayers()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        if (vehicles.Length < 4)
        {
            for (int i = vehicles.Length; i < 4; i++)
            {
                Instantiate(AIVehicle, spawnManager.GetComponent<playerspawnmanager>().spawnLocations[i].position, spawnManager.GetComponent<playerspawnmanager>().spawnLocations[i].rotation);
            }
            int j = vehicles.Length;
            foreach (GameObject AIVehicle in GameObject.FindGameObjectsWithTag("AIPlayer"))
            {
                AIVehicle.transform.GetChild(0).GetComponent<VehicleData>().playerID = j;
                j++;
            }
        }
        else
        {
            playerMessage.text = "Maximum Amount of Players Already!";
            StartCoroutine(nameof(ShortMessage));
        }
        spawnManager.GetComponent<PlayerInputManager>().DisableJoining();
        pauseMenu.SetActive(false);
        foreach (GameObject prompt in prompts)
        {
            prompt.SetActive(true);
        }
    }

    IEnumerator ShortMessage()
    {
        yield return new WaitForSeconds(2);
        playerMessage.text = "";
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        foreach (GameObject prompt in prompts)
        {
            prompt.SetActive(!pauseMenu.activeSelf);
        }
        resetButton.Select();
    }

    public void ResetPlayerJoin()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            Destroy(vehicle.transform.parent.gameObject);
        }
        foreach (GameObject AIVehicle in GameObject.FindGameObjectsWithTag("AIPlayer"))
        {
            Destroy(AIVehicle);
        }
        spawnManager.GetComponent<PlayerInputManager>().EnableJoining();
        spawnManager.GetComponent<playerspawnmanager>().index = 0;
        spawnManager.GetComponent<playerspawnmanager>().manager.playerPrefab = spawnManager.GetComponent<playerspawnmanager>().vehicles[0];
        foreach (GameObject prompt in prompts)
        {
            prompt.SetActive(true);
        }
        pauseMenu.SetActive(false);
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
