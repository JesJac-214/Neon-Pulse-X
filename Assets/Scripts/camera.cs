using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    //public GameObject Vehicle;

    void LateUpdate()
    {
        //transform.position = Vehicle.transform.position + new Vector3(0, 50, -5);
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        GameObject leadVehicle = vehicles[0];
        foreach (GameObject vehicle in vehicles)
        {
            if (vehicle.GetComponent<vehicle>().courseProgress > leadVehicle.GetComponent<vehicle>().courseProgress)
            {
                leadVehicle = vehicle;
            }
        }

        transform.position = leadVehicle.transform.position + new Vector3(0, 50, -5);
    }
}
