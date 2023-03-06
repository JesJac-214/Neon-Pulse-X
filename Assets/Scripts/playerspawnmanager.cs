using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerspawnmanager : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] vehicles;
    PlayerInputManager manager;
    private int index = 0;

    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        manager.playerPrefab = vehicles[index];
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.gameObject.GetComponent<VehicleData>().playerID = playerInput.playerIndex;
        playerInput.gameObject.GetComponent<VehicleData>().startTransform = spawnLocations[playerInput.playerIndex];
        index++;
        manager.playerPrefab = vehicles[index];
    }
}
