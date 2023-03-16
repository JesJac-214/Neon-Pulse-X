using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    Checkpoint[] checkpoints;
    int totalCheckpoints;
    public float requiredLapCompletion = 0.6f;
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
        totalCheckpoints = checkpoints.Length;
    }

    public void ResetCheckpoints(int playerID)
    {
        int checkpointsCrossed = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (Checkpoint checkpoint in checkpoints)
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
                VehicleData playerData = player.GetComponent<VehicleData>();
                if (playerData.playerID == playerID)
                {
                    playerData.courseProgress += (totalCheckpoints - checkpointsCrossed);
                    playerData.laps++;
                }
            }
            foreach (Checkpoint checkpoint in checkpoints)
            {
                checkpoint.collided[playerID] = false;
                checkpoint.RespawnPositionTrackerHit = false;
            }
        }
    }
}
