using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField]
    private int minPlayers = 2;

    [SerializeField]
    private TMP_Text[] ReadyTexts;

    [SerializeField]
    private TMP_Text[] PlayerTitles;

    [SerializeField]
    private TMP_Text JoinPrompt;

    [SerializeField]
    private TMP_Text[] ItemHUDs;

    [SerializeField]
    private TMP_Text ReadyAmount;

    private int readyPlayers = 0;

    GameObject[] vehicles;

    private bool countdownRunning = false;

    public AudioSource countdownChime;

    void Update()
    {
        readyPlayers = 0;
        vehicles = GameObject.FindGameObjectsWithTag("Player");
        if (vehicles.Length == 1)
        {
            JoinPrompt.text = "";
        }
        foreach (GameObject vehicle in vehicles)
        {
            PlayerTitles[vehicle.GetComponent<VehicleData>().playerID].text = "Player " + (vehicle.GetComponent<VehicleData>().playerID + 1).ToString();
            ItemHUDs[vehicle.GetComponent<VehicleData>().playerID].text = (vehicle.GetComponent<VehicleWeaponItemLogic>().Item.ammo).ToString();
            if (vehicle.GetComponent<VehicleData>().isReady)
            {
                ReadyTexts[vehicle.GetComponent<VehicleData>().playerID].text = "Ready!";
            }
            else
            {
                ReadyTexts[vehicle.GetComponent<VehicleData>().playerID].text = "";
            }
        }
        foreach (GameObject vehicle in vehicles)
        {
            if (vehicle.GetComponent<VehicleData>().isReady)
            {
                readyPlayers++;
            }
        }
        if (vehicles.Length >= minPlayers)
        {
            if (readyPlayers == vehicles.Length)
            {
                if (!countdownRunning)
                {
                    StartCoroutine(nameof(DelayedJoin));
                    countdownRunning = true;
                }
            }
            else
            {
                StopCoroutine(nameof(DelayedJoin));
                countdownRunning = false;
                JoinPrompt.text = "";
            }
        }
        if (vehicles.Length > 0)
        {
            ReadyAmount.text = "(" + readyPlayers + "/" + vehicles.Length + ") Ready!";
            if (readyPlayers == vehicles.Length && readyPlayers == 1)
            {
                ReadyAmount.text = "2 Players Minimum!";
            }
        }
    }

    IEnumerator DelayedJoin()
    {
        for (int i = 3; i > 0; i--)
        {
            JoinPrompt.text = "Loading track in " + i.ToString();
            countdownChime.Play();
            yield return new WaitForSeconds(1);
        }
        foreach (GameObject vehicle in vehicles)
        {
            DontDestroyOnLoad(vehicle.transform.parent.gameObject);
            vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            vehicle.GetComponent<VehicleWeaponItemLogic>().Item = new EquipmentBase();
        }
        SceneManager.LoadScene("Real_track 2");
    }
}
