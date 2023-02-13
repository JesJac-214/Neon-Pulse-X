using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public bool[] collided = {false, false, false, false};

    public bool test = false;

    void OnTriggerEnter(Collider other)
    {
        int playerID = other.transform.parent.GetComponent<vehicle>().playerID;
        
        if (!collided[playerID])
        {
            // !!IMPORTANT!! This may be a bit wack so check back and fix it if it has to be fixed
            other.transform.parent.BroadcastMessage("IncrementProgress");
        }
        if (collided[playerID])
        {
            other.transform.parent.BroadcastMessage("DecrementProgress");
        }
        collided[playerID] = !collided[playerID];
    }
}
