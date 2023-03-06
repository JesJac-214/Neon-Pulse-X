using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;
    GameObject[] vehicles;
    [SerializeField]
    private TMP_Text countdownText;
    void Start()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            vehicle.transform.SetPositionAndRotation(spawns[vehicle.GetComponent<VehicleData>().playerID].position, spawns[vehicle.GetComponent<VehicleData>().playerID].rotation);
            vehicle.GetComponent<VehicleDrivingAimingLogic>().canAccel = false;
        }
        StartCoroutine(nameof(AccelDelay));
    }

    IEnumerator AccelDelay()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "DON'T FALL BEHIND";
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.GetComponent<VehicleDrivingAimingLogic>().canAccel = true;
        }
        yield return new WaitForSeconds(1);
        countdownText.text = "";
    }
}
