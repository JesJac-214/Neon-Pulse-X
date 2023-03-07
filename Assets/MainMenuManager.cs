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
    public Transform PlayerJoinTransform;
    private Transform target;

    public GameObject MainMenu;
    public GameObject PlayerJoin;

    public AudioSource audioSource;
    public AudioSource buttonPress;

    public float volume = 0.5f;

    private bool atPressAnyButton = true;
    private bool move = false;

    void Update()
    {
        if (atPressAnyButton)
        {
            InputSystem.onAnyButtonPress.CallOnce(_ => MoveCameraToMainMenu());
            atPressAnyButton = false;
        }
        if (move)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, target.position, 4f * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, target.rotation, 8f * Time.deltaTime);
            if ((cam.transform.position - BillboardTransform.position).magnitude < 50f)
            {
                MainMenu.SetActive(true);
            }
            else
            {
                MainMenu.SetActive(false);
            }
            if ((cam.transform.position - target.position).magnitude < 0.1f)
            {
                move = false;
            }
        }
    }

    void MoveCameraToMainMenu()
    {
        PressAnyButton.SetActive(false);
        audioSource.Play();
        target = BillboardTransform;
        move = true;
    }

    public void OnPlay()
    {
        target = PlayerJoinTransform;
        move = true;
        PlayerJoin.SetActive(true);
        buttonPress.Play();
    }

    public void OnQuit()
    {
        StartCoroutine(nameof(DelayedQuit));
    }

    IEnumerator DelayedQuit()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
