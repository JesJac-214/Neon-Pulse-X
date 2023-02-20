using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerspawnmanager : MonoBehaviour
{
    public Transform[] spawnLocations;
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.gameObject.GetComponent<VehicleData>().playerID = playerInput.playerIndex;
        playerInput.gameObject.GetComponent<VehicleData>().startPos = spawnLocations[playerInput.playerIndex].position;
    }
}
