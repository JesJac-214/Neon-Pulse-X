using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeamLogic : MonoBehaviour
{
    [SerializeField] private float launchVelocity = 100f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * launchVelocity;
        Destroy(gameObject, 5);
    }

 
}
