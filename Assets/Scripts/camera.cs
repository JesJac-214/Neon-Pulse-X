using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    //public GameObject Vehicle;
    Vector3 positionVelocity = Vector3.zero;
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

        //transform.position = leadVehicle.transform.position + new Vector3(0, 50, -5);
        transform.position = Vector3.SmoothDamp(transform.position, leadVehicle.transform.position + new Vector3(0, 50, -20), ref positionVelocity, 0.2f);
    }
}
