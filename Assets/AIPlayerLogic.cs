using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIPlayerLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnGameStart;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnGameStart;
    }
    private void OnGameStart(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Real_track 2")
        {
            GetComponent<VehicleDrivingAimingLogic>().accelerateInput = 1;
        }
    }

    public void SteerAI(Quaternion rot)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VehicleWeaponItemLogic>().Item.ammo > 0)
        {
            GetComponent<VehicleWeaponItemLogic>().Item.Use(gameObject);
        }
    }
}
