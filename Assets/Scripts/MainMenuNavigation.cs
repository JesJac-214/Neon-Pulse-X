using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    public void OnMainMenuQuit()
    {
        Application.Quit();
    }

    public void OnPressedPlay()
    {
        SceneManager.LoadScene("Track");
    }
}
