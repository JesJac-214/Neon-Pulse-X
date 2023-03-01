using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;
    void Start()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.transform.position = spawns[vehicle.GetComponent<VehicleData>().playerID].position;
            vehicle.transform.rotation = spawns[vehicle.GetComponent<VehicleData>().playerID].rotation;
        }
    }
}
