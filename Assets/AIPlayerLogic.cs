using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIPlayerLogic : MonoBehaviour
{
    public GameObject[] frontTires;
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
        foreach (GameObject tire in frontTires)
        {
            tire.transform.rotation = rot;
        }
    }

    bool shooting = false;
    void Update()
    {
        if (GetComponent<VehicleWeaponItemLogic>().Item.ammo > 0 && !shooting)
        {
            StartCoroutine(nameof(ItemUseDelay));
            shooting = true;
        }
    }

    IEnumerator ItemUseDelay()
    {
        for (int i = GetComponent<VehicleWeaponItemLogic>().Item.ammo; i > 0; i--)
        {
            GetComponent<VehicleWeaponItemLogic>().Item.Use(gameObject);
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
        shooting = false;
    }
}
