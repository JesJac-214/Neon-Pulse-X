using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetManagement : MonoBehaviour
{
    public GameObject target;
    public Camera cam;
    public GameObject RespawnPositionTracker;

    public GameObject SpawnEffect;

    public AudioSource heartBreak;

    public TMP_Text[] deathCountdownTimers;

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
    public IEnumerator DelayedDeath(GameObject vehicle)
    {
        for (int i = 3; i > 0; i--)
        {
            deathCountdownTimers[vehicle.GetComponent<VehicleData>().playerID].text = i.ToString();
            //countdownChime.Play();
            yield return new WaitForSeconds(1);
        }
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        VehicleData vehicleData = vehicle.GetComponent<VehicleData>();
        if (vehicleData.lives > 0)
        {
            vehicleData.lives--;
            heartBreak.Play();
            vehicle.transform.SetPositionAndRotation(RespawnPositionTracker.transform.position, RespawnPositionTracker.transform.rotation);
            vehicleData.courseProgress = RespawnPositionTracker.GetComponent<RespawnPositionTracker>().courseProgress;
            FindObjectOfType<CheckpointManager>().SetCheckpointsToRespawnTracker(vehicleData.playerID);
            Destroy(Instantiate(SpawnEffect, RespawnPositionTracker.transform.position + new Vector3(0, -7, 0), RespawnPositionTracker.transform.rotation), 2);
            vehicle.GetComponent<Rigidbody>().velocity = vehicle.transform.forward * 200;
            playerDying[vehicleData.playerID] = false;
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
            deathCountdownTimers[vehicle.GetComponent<VehicleData>().playerID].text = "X";
        }
    }

    private bool[] playerDying = { false, false, false, false };
    IEnumerator[] playerDeathInsts = { null, null, null, null};

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
                if (!IsVisible(cam, vehicle) || vehicle.transform.position.y < -5)
                {
                    if (!playerDying[vehicle.GetComponent<VehicleData>().playerID])
                    {
                        playerDeathInsts[vehicle.GetComponent<VehicleData>().playerID] = DelayedDeath(vehicle);
                        StartCoroutine(playerDeathInsts[vehicle.GetComponent<VehicleData>().playerID]);
                        playerDying[vehicle.GetComponent<VehicleData>().playerID] = true;
                    }
                }
                else
                {
                    //vehicle.GetComponent<VehicleData>().StopAllCoroutines();
                    if (playerDeathInsts[vehicle.GetComponent<VehicleData>().playerID] != null)
                    {
                        StopCoroutine(playerDeathInsts[vehicle.GetComponent<VehicleData>().playerID]);
                    }
                    playerDying[vehicle.GetComponent<VehicleData>().playerID] = false;
                    deathCountdownTimers[vehicle.GetComponent<VehicleData>().playerID].text = "";
                }
            }
        }
    }
}
