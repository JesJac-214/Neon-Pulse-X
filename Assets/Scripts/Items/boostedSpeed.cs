using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostedSpeed : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle Body")
        {
            Debug.Log("speed boost");
        }
    }
}
