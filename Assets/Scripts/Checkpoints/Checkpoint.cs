using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool[] collided = {false, false, false, false};

    public bool test = false;

    public bool RespawnPositionTrackerHit = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            if (other.gameObject.transform.parent.transform.parent.gameObject.CompareTag("AIPlayer"))
            {
                Debug.Log("AI");
            }
            // !!IMPORTANT!! This may be a bit wack so check back and fix it if it has to be fixed
            Transform player = other.transform.parent;
            int PlayerID = player.GetComponent<VehicleData>().playerID;

            player.GetComponent<VehicleData>().lastHitCheckpointTransform = transform;

            if (!collided[PlayerID])
            {
                player.BroadcastMessage("IncrementProgress");
            }
            if (collided[PlayerID])
            {
                player.BroadcastMessage("DecrementProgress");
            }
            collided[PlayerID] = !collided[PlayerID];
        }
        if (other.gameObject.CompareTag("RespawnPositionTrackerCube"))
        {
            if (!RespawnPositionTrackerHit)
            {
                other.GetComponent<RespawnPositionTracker>().IncrementProgress();
            }
            if (RespawnPositionTrackerHit)
            {
                other.GetComponent<RespawnPositionTracker>().DecrementProgress();
            }
            other.transform.rotation = transform.rotation * Quaternion.Euler(-90,-90,-270); // This constant is so that the rotation has the Z axis forward so that the vehicles can use it as spawn
            RespawnPositionTrackerHit = !RespawnPositionTrackerHit;
        }
    }
}
