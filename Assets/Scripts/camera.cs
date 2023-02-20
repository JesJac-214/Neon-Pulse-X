using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector3 positionVelocity = Vector3.zero;
    void LateUpdate()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        if (vehicles.Length > 0)
        {
            GameObject leadVehicle = vehicles[0];
            foreach (GameObject vehicle in vehicles)
            {
                if (vehicle.GetComponent<VehicleData>().courseProgress > leadVehicle.GetComponent<VehicleData>().courseProgress)
                {
                    leadVehicle = vehicle;
                }
            }
            transform.position = Vector3.SmoothDamp(transform.position, leadVehicle.transform.position + new Vector3(0, 50, -20), ref positionVelocity, 0.5f);
        }
    }
}
