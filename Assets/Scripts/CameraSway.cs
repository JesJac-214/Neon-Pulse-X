using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float amplitude = 0.5f;

    void Update()
    {
        transform.position += amplitude * Time.deltaTime * new Vector3(0, Mathf.Cos(Time.time), Mathf.Sin(Time.time));
    }
}
