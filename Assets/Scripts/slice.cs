using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slice : MonoBehaviour
{
    bool[] collided = {false, false, false, false};

    void OnTriggerEnter(Collider other)
    {
        if (!collided[other.transform.parent.GetComponent<vehicle>().playerID])
        {
            // !!IMPORTANT!! This may be a bit wack so check back and fix it if it has to be fixed
            other.transform.parent.BroadcastMessage("IncrementProgress");
        }
        if (collided[other.transform.parent.GetComponent<vehicle>().playerID])
        {
            other.transform.parent.BroadcastMessage("DecrementProgress");
        }
        collided[other.transform.parent.GetComponent<vehicle>().playerID] = !collided[other.transform.parent.GetComponent<vehicle>().playerID];
    }
}
