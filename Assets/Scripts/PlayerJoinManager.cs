using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField]
    private int minPlayers = 2;

    [SerializeField]
    private TMP_Text[] ReadyTexts;

    [SerializeField]
    private TMP_Text[] PlayerTitles;

    [SerializeField]
    private TMP_Text JoinPrompt;

    private int readyPlayers = 0;

    GameObject[] vehicles;

    void Update()
    {
        readyPlayers = 0;
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        if (vehicles.Length > 0)
        {
            JoinPrompt.text = "";
        }
        foreach (GameObject vehicle in vehicles)
        {
            PlayerTitles[vehicle.GetComponent<VehicleData>().playerID].text = "Player " + (vehicle.GetComponent<VehicleData>().playerID + 1).ToString();
            if (vehicle.GetComponent<VehicleData>().isReady)
            {
                ReadyTexts[vehicle.GetComponent<VehicleData>().playerID].text = "Ready!";
            }
            else
            {
                ReadyTexts[vehicle.GetComponent<VehicleData>().playerID].text = "Enter Ready Zone";
            }
        }
        if (vehicles.Length >= minPlayers)
        {
            foreach (GameObject vehicle in vehicles)
            {
                if (vehicle.GetComponent<VehicleData>().isReady)
                {
                    readyPlayers++;
                }
            }
            if (readyPlayers == vehicles.Length)
            {
                foreach (GameObject vehicle in vehicles)
                {
                    DontDestroyOnLoad(vehicle.transform.parent.gameObject);
                    vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                SceneManager.LoadScene("Real_track 2");
            }
        }
    }
}
