using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    private float amplitude = 0.005f;
    void Update()
    {
        transform.position += new Vector3(amplitude * Mathf.Cos(Time.time) * 0, amplitude * Mathf.Cos(Time.time), amplitude * Mathf.Sin(Time.time));
    }
}
