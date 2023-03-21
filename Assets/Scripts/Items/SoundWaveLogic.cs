using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveLogic : MonoBehaviour
{
    [SerializeField] private float launchVelocity = 150f;
    [SerializeField] private float liftForce = 35f;
    public GameObject SoundwaveParticle;
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
            if (!other.transform.parent.GetComponent<VehicleData>().isShielded)
            {
                rb.velocity = transform.up * liftForce;
            }
            Destroy(Instantiate(SoundwaveParticle, other.transform.position, transform.rotation), 2);
        }
        
    }
}