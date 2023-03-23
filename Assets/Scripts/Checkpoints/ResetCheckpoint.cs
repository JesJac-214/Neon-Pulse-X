using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCheckpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "VehicleShape")
        {
            GameObject.Find("Checkpoint Manager").BroadcastMessage("ResetCheckpoints", other.transform.parent.GetComponent<VehicleData>().playerID);
        }
        if (other.CompareTag("RespawnPositionTrackerCube"))
        {
            GameObject.Find("Checkpoint Manager").GetComponent<CheckpointManager>().ResetCubeCheckpoints(other.gameObject);
        }
    }
}
