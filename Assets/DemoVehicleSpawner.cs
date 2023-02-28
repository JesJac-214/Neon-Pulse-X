using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoVehicleSpawner : MonoBehaviour
{
    public GameObject demoVehiclePrefab;
    public Transform[] spawnLocations;

    public int suspensionAccelerationDemoDelay = 3;

    private float newTime;

    void Start()
    {
        newTime = Time.time + suspensionAccelerationDemoDelay;
        Instantiate(demoVehiclePrefab, spawnLocations[0]);
    }

    void Update()
    {
        if (Time.time < newTime) { return; }
        Instantiate(demoVehiclePrefab, spawnLocations[0]);
        newTime = Time.time + suspensionAccelerationDemoDelay;
    }
}
