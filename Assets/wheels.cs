using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheels : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 previous;

    void Update()
    {
        velocity = ((transform.position - previous)) / Time.deltaTime;
        previous = transform.position;
    }
}
