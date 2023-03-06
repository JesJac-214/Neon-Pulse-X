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

    public AudioSource audioSource;

    public float volume = 0.5f;

    private bool move = false;

    void Update()
    {
        if (!move)
        {
            InputSystem.onAnyButtonPress.CallOnce(_ => MoveCameraToMainMenu());
        }
        if (move)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, BillboardTransform.position, 4f * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, BillboardTransform.rotation, 8f * Time.deltaTime);
            if ((cam.transform.position - BillboardTransform.position).magnitude < 50f)
            {
                menu.SetActive(true);
            }
            if ((cam.transform.position - BillboardTransform.position).magnitude < 0.1f)
            {
                enabled = false;
            }
        }
    }

    void MoveCameraToMainMenu()
    {
        PressAnyButton.SetActive(false);
        audioSource.Play();
        move = true;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("PlayerJoin");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
