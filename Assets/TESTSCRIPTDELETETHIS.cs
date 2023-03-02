using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using TMPro;

public class TESTSCRIPTDELETETHIS : MonoBehaviour
{
    public TMP_Text pranbu;
    public GameObject cam;
    public Transform goal;

    public GameObject menu;

    private bool move = false;
    void Update()
    {
        if (!move)
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
                enabled = false;
            }
        }
    }

    void MoveCameraToMainMenu()
    {
        move = true;
        pranbu.text = "Select Stuffs";
    }

    public void OnPlay()
    {
        Debug.Log("Play");
    }

    public void OnQuit()
    {
        Debug.Log("Quit");
    }
}
