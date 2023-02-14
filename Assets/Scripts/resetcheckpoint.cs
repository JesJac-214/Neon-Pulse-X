using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetcheckpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "VehicleShape")
        {
            GameObject.Find("Checkpoint Manager").BroadcastMessage("ResetCheckpoints", other.transform.parent.GetComponent<vehicle>().playerID);
            other.transform.parent.GetComponent<vehicle>().ResetAmmo();
        }
    }
}
