using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public void OnRestartGame()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject vehicle in vehicles)
        {
            vehicle.GetComponent<VehicleData>().lives = 3;
            vehicle.GetComponent<VehicleData>().courseProgress = 0;
            vehicle.GetComponent<VehicleData>().isDead = false;
            vehicle.GetComponent<VehicleData>().placement = 0;
            vehicle.GetComponent<VehicleWeaponItemLogic>().Item = new EquipmentBase();
        }
        SceneManager.LoadScene("Real_track 2");
    }

    public void OnReturnToMainMenu()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject vehicle in vehicles)
        {
            Destroy(vehicle.transform.parent.gameObject);
        }
        
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
