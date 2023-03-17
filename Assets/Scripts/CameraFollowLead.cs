using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowLead : MonoBehaviour
{
    Vector3 positionVelocity = Vector3.zero;

    public Vector3 cameraOffset = new(0, 100, -40);
    [SerializeField]
    private float cameraFollowLag = 50f;
    public GameObject RespawnPositionTracker;
    public bool GameWon = false;

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
            if (!GameWon)
            {
                transform.position = Vector3.SmoothDamp(transform.position, leadVehicle.GetComponent<VehicleData>().lastHitCheckpointTransform.position + cameraOffset, ref positionVelocity, cameraFollowLag * Time.smoothDeltaTime);
                RespawnPositionTracker.transform.position = transform.position - cameraOffset;
            }
            if (GameWon)
            {
                transform.position = Vector3.SmoothDamp(transform.position, leadVehicle.transform.position + cameraOffset * 0.5f, ref positionVelocity, cameraFollowLag * 0.25f * Time.smoothDeltaTime);
            }
        }
    }
}
