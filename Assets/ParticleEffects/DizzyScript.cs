using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyScript : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, -1, 0 * Time.deltaTime);
    }
}
