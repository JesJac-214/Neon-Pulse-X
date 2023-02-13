using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointManager : MonoBehaviour
{
    checkpoint[] checkpoints;
    int totalCheckpoints;
    public float requiredLapCompletion = 0.6f;
    void Start()
    {
        checkpoints = FindObjectsOfType<checkpoint>();
        totalCheckpoints = checkpoints.Length;
    }

    void ResetCheckpoints(int playerID)
    {
        int checkpointsCrossed = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.collided[playerID])
            {
                checkpointsCrossed++;
            }
        }
        
        if (checkpointsCrossed > requiredLapCompletion * totalCheckpoints)
        {
            foreach (GameObject player in players)
            {
                if (player.GetComponent<vehicle>().playerID == playerID)
                {
                    player.GetComponent<vehicle>().courseProgress += (totalCheckpoints - checkpointsCrossed);
                    player.GetComponent<vehicle>().laps++;
                }
            }
            foreach (checkpoint checkpoint in checkpoints)
            {
                checkpoint.collided[playerID] = false;
            }
        }
    }
}
