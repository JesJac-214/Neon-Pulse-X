using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    //Transform[] childArray;
    slice[] slices;
    int totalCheckpoints;
    void Start()
    {
        //childArray = GetComponentsInChildren<Transform>();
        slices = FindObjectsOfType<slice>();
        totalCheckpoints = slices.Length;
    }

    void ResetCheckpoints(int playerID)
    {
        //Debug.Log("Collided with " + playerID);
        int checkpointsCrossed = 0;
        foreach (slice slice in slices)
        {
            if (slice.collided[playerID])
            {
                checkpointsCrossed++;
            }
        }
        
        foreach (slice slice in slices)
        {
            slice.collided[playerID] = false;
        }
    }
}
