using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    Transform[] childArray;
    slice[] slices;
    void Start()
    {
        childArray = GetComponentsInChildren<Transform>();
        slices = FindObjectsOfType<slice>();
    }

    void ResetCheckpoints(int playerID)
    {
        Debug.Log("Collided with " + playerID);
        foreach (slice slice in slices)
        {
            slice.collided[playerID] = false;
        }
    }
}
