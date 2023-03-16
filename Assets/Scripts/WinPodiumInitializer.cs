using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPodiumInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;
    void Start()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            vehicle.GetComponent<VehicleDrivingAimingLogic>().canAccel = false;
            vehicle.transform.position = spawns[vehicle.GetComponent<VehicleData>().placement].position;
            vehicle.transform.rotation = spawns[vehicle.GetComponent<VehicleData>().placement].rotation;
            vehicle.GetComponent<VehicleWeaponItemLogic>().Item = new EquipmentBase();
        }
    }
}
