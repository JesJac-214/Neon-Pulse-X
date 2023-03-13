using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveLogic : MonoBehaviour
{
    [SerializeField] private float launchVelocity = 100f;
    [SerializeField] private float liftForce = 25f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * launchVelocity;
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            Rigidbody rb = other.transform.parent.GetComponent<Rigidbody>();
            rb.velocity = transform.up * liftForce;
        }
        
    }
}