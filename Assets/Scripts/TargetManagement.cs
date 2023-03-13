using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagement : MonoBehaviour
{
    public GameObject target;
    public Camera cam;

    public AudioSource heartBreak;
    
    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach(var plane in planes)
        {
            if(plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
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

            foreach (GameObject vehicle in vehicles)
            {
                if (!IsVisible(cam, vehicle) || vehicle.transform.position.y < -25)
                {
                    if (vehicle.GetComponent<VehicleData>().lives > 0)
                    {
                        vehicle.GetComponent<VehicleData>().lives--;
                        heartBreak.Play();
                        vehicle.transform.position = cam.transform.position -  new Vector3(0, 100, -40);
                        vehicle.GetComponent<VehicleData>().courseProgress = leadVehicle.GetComponent<VehicleData>().courseProgress;
                        vehicle.transform.rotation = leadVehicle.transform.rotation;
                    }
                    else
                    {
                        vehicle.transform.position = new Vector3(0, -50, 0);
                        vehicle.GetComponent<VehicleData>().courseProgress = 0;
                        vehicle.GetComponent<VehicleData>().isDead = true;
                        int aliveCount = 0;
                        foreach (GameObject car in vehicles)
                        {
                            if (!car.GetComponent<VehicleData>().isDead)
                            {
                                aliveCount++;
                            }
                        }
                        if (vehicle.GetComponent<VehicleData>().placement == 0)
                        { 
                            vehicle.GetComponent<VehicleData>().placement = aliveCount;
                        }
                    }
                }
            }
        }
    }
}
