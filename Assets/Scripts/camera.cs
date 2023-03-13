using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector3 positionVelocity = Vector3.zero;

    [SerializeField]
    private Vector3 cameraOffset = new(0, 100, -40);
    [SerializeField]
    private float cameraFollowLag = 50f;

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
            transform.position = Vector3.SmoothDamp(transform.position, leadVehicle.GetComponent<VehicleData>().lastHitCheckpointTransform.position + cameraOffset, ref positionVelocity, cameraFollowLag * Time.smoothDeltaTime);
        }
    }
}
