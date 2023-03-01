using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField]
    private int minPlayers = 2;
    private int readyPlayers = 0;
    void Update()
    {
        readyPlayers = 0;
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        if (vehicles.Length >= minPlayers)
        {
            foreach (GameObject vehicle in vehicles)
            {
                if (vehicle.GetComponent<VehicleData>().isReady)
                {
                    readyPlayers++;
                }
            }
            Debug.Log(vehicles.Length + " players\n" + readyPlayers + " ready!");
            if (readyPlayers == vehicles.Length)
            {
                foreach (GameObject vehicle in vehicles)
                {
                    DontDestroyOnLoad(vehicle.transform.parent.gameObject);
                }
                SceneManager.LoadScene("Track2");
            }
        }
    }
}
