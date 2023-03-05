using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;
    GameObject[] vehicles;
    void Start()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            vehicle.transform.position = spawns[vehicle.GetComponent<VehicleData>().playerID].position;
            vehicle.transform.rotation = spawns[vehicle.GetComponent<VehicleData>().playerID].rotation;
            vehicle.GetComponent<VehicleDrivingAimingLogic>().canAccel = false;
        }
        StartCoroutine("AccelDelay");
    }

    IEnumerator AccelDelay()
    {
        yield return new WaitForSeconds(3);
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.GetComponent<VehicleDrivingAimingLogic>().canAccel = true;
        }
    }
}
