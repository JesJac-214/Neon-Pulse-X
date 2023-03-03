using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject PressAnyButton;
    public GameObject cam;
    public Transform BillboardTransform;

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
            cam.transform.position = Vector3.Lerp(cam.transform.position, BillboardTransform.position, 0.01f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, BillboardTransform.rotation, 0.01f);
            if ((BillboardTransform.position.z + 0.2f > cam.transform.position.z && cam.transform.position.z > BillboardTransform.position.z - 0.2f) && (BillboardTransform.position.y + 0.2f > cam.transform.position.y && cam.transform.position.y > BillboardTransform.position.y - 0.2f) && (BillboardTransform.position.x + 0.2f > cam.transform.position.x && cam.transform.position.x > BillboardTransform.position.x - 0.2f))
            {
                menu.SetActive(true);
                enabled = false;
            }
        }
    }

    void MoveCameraToMainMenu()
    {
        move = true;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("PlayerJoin");
    }

    public void OnQuit()
    {
        Debug.Log("Quit");
    }
}
