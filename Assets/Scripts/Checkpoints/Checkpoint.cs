using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Checkpoint : MonoBehaviour
{
    public bool[] collided = {false, false, false, false};

    public bool test = false;

    public bool RespawnPositionTrackerHit = false;


    private float[] PlayerCollisionPositionX = { 0, 0, 0, 0 };


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            Transform player = other.transform.parent;
            int PlayerID = player.GetComponent<VehicleData>().playerID;
            PlayerCollisionPositionX[PlayerID] = Vector3.Project((other.transform.position - transform.position), transform.up).normalized.x;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        //Debug.Log("Exit:" + Vector3.Project((other.transform.position - transform.position), transform.up).normalized.x);
        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            // !!IMPORTANT!! This may be a bit wack so check back and fix it if it has to be fixed
            Transform player = other.transform.parent;
            int PlayerID = player.GetComponent<VehicleData>().playerID;
            Debug.Log(Mathf.Round(PlayerCollisionPositionX[PlayerID] * 100) * 0.01);
            if (Mathf.Round(Vector3.Project((other.transform.position - transform.position), transform.up).normalized.x * 100) * 0.01 == Mathf.Round(PlayerCollisionPositionX[PlayerID] * 100) * 0.01)
            {
                Debug.Log("Did not drive through!\n" + Mathf.Round(Vector3.Project((other.transform.position - transform.position), transform.up).normalized.x * 100) * 0.01 + " == " + Mathf.Round(PlayerCollisionPositionX[PlayerID] * 100) * 0.01);
            }
            else
            {
                Debug.Log("Drove through!\n" + Vector3.Project((other.transform.position - transform.position), transform.up).normalized.x + " != " + PlayerCollisionPositionX[PlayerID]);
                if (other.gameObject.transform.parent.transform.parent.gameObject.CompareTag("AIPlayer"))
                {
                    other.gameObject.transform.parent.GetComponent<AIPlayerLogic>().SteerAI(transform.rotation * Quaternion.Euler(-180, -90, -270));
                }

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
