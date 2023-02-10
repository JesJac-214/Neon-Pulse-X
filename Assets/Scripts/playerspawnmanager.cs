using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerspawnmanager : MonoBehaviour
{
    public Transform[] spawnLocations;
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
        playerInput.gameObject.GetComponent<vehicle>().playerID = playerInput.playerIndex;
        playerInput.gameObject.GetComponent<vehicle>().startPos = spawnLocations[playerInput.playerIndex].position;
    }
}
