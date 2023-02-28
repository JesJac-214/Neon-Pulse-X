using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DemoCamera : MonoBehaviour
{
    public Transform[] positions;
    private string[] titles = {"Suspension and Acceleration", "Steering"};
    public TMP_Text title;


    private int position = 0;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, positions[position].transform.position, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, positions[position].transform.rotation, 0.05f);
        title.text = titles[position];
    }

    public void OnNextSlide(InputAction.CallbackContext context)
    {
        Debug.Log("Next");
        if (context.ReadValue<float>() == 0)
        {
            if (position < positions.Length-1)
            {
                position++;
            }
            else
            {
                position = 0;
            }
        }
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
