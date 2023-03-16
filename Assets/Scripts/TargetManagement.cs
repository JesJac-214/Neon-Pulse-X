using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagement : MonoBehaviour
{
    public GameObject target;
    public Camera cam;
    public GameObject RespawnPositionTracker;

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
                    VehicleData vehicleData = vehicle.GetComponent<VehicleData>();
                    if (vehicleData.lives > 0)
                    {
                        vehicleData.lives--;
                        heartBreak.Play();
                        //vehicle.transform.SetPositionAndRotation(cam.transform.position - cam.GetComponent<CameraFollowLead>().cameraOffset, leadVehicle.transform.rotation);
                        vehicle.transform.SetPositionAndRotation(RespawnPositionTracker.transform.position, RespawnPositionTracker.transform.rotation);
                        //vehicleData.courseProgress = leadVehicle.GetComponent<VehicleData>().courseProgress;
                        vehicleData.courseProgress = RespawnPositionTracker.GetComponent<RespawnPositionTracker>().courseProgress;
                        vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        //vehicle.transform.position = cam.transform.position - cam.GetComponent<CameraFollowLead>().cameraOffset;
                        //vehicle.transform.rotation = leadVehicle.transform.rotation;
                    }
                    else
                    {
                        vehicle.transform.position = new Vector3(0, -50, 0);
                        vehicleData.courseProgress = 0;
                        vehicleData.isDead = true;
                        int aliveCount = 0;
                        foreach (GameObject car in vehicles)
                        {
                            if (!car.GetComponent<VehicleData>().isDead)
                            {
                                aliveCount++;
                            }
                        }
                        if (vehicleData.placement == 0)
                        { 
                            vehicleData.placement = aliveCount;
                        }
                    }
                }
            }
        }
    }
}
