using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazzyScript1 : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, -120 * Time.deltaTime);
    }
}
