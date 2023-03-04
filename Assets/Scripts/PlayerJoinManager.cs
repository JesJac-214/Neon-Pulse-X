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

    private int readyPlayers = 0;
    void Update()
    {
        readyPlayers = 0;
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            PlayerTitles[vehicle.GetComponent<VehicleData>().playerID].text = "Player " + (vehicle.GetComponent<VehicleData>().playerID + 1).ToString();
            if (vehicle.GetComponent<VehicleData>().isReady)
            {
                ReadyTexts[vehicle.GetComponent<VehicleData>().playerID].text = "Ready!";
            }
            else
            {
                ReadyTexts[vehicle.GetComponent<VehicleData>().playerID].text = "Press A to Ready Up!";
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
