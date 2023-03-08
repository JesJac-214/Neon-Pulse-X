using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using TMPro;
using UnityEngine.UI;

public class TESTSCRIPTDELETETHIS : MonoBehaviour
{
    public TMP_Text pranbu;
    public GameObject cam;
    public Transform goal;

    public GameObject menu;
    public GameObject select;

    private bool move = false;
    private bool pranbu2 = true;

    public Button track1;
    public Button play;

    void Update()
    {
        if (!move && pranbu2)
        {
           InputSystem.onAnyButtonPress.CallOnce(_ => MoveCameraToMainMenu());
        }
        if (move)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, goal.position, 0.01f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, goal.rotation, 0.01f);
            if ((goal.position.z + 0.2f > cam.transform.position.z && cam.transform.position.z > goal.position.z - 0.2f) && (goal.position.y + 0.2f > cam.transform.position.y && cam.transform.position.y > goal.position.y - 0.2f) && (goal.position.x + 0.2f > cam.transform.position.x && cam.transform.position.x > goal.position.x - 0.2f))
            {
                menu.SetActive(true);
            }
        }
        if (Gamepad.current.bButton.isPressed && select.activeInHierarchy)
        {
            select.SetActive(false);
            menu.SetActive(true);
            play.Select();
        }
    }

    void MoveCameraToMainMenu()
    {
        pranbu2 = false;
        move = true;
        pranbu.text = "Select Stuffs";
    }

    public void OnPlay()
    {
        menu.SetActive(false);
        move = false;
        select.SetActive(true);
        track1.Select();
    }

    public void OnQuit()
    {
        Debug.Log("Quit");
    }

    public void OnTrack1()
    {
        Debug.Log("Track 1");
    }

    public void OnTrack2()
    {
        Debug.Log("Track 2");
    }

    public void OnTrack3()
    {
        Debug.Log("Track 3");
    }
}
