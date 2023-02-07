using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * 30 * Time.deltaTime;
    }
}
