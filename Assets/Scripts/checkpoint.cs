using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public bool[] collided = {false, false, false, false};

    public bool test = false;

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.transform.parent;
        int PlayerID = player.GetComponent<vehicle>().playerID;
        
        if (!collided[PlayerID])
        {
            // !!IMPORTANT!! This may be a bit wack so check back and fix it if it has to be fixed
            player.BroadcastMessage("IncrementProgress");
        }
        if (collided[PlayerID])
        {
            player.BroadcastMessage("DecrementProgress");
        }
        collided[PlayerID] = !collided[PlayerID];
    }
}
