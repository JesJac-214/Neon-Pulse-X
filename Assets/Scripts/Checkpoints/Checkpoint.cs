using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool[] collided = {false, false, false, false};

    public bool test = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle Body"))
        {
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
    }
}
