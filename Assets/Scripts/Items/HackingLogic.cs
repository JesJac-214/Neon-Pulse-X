using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingLogic : MonoBehaviour
{
    [SerializeField] private float launchVelocity = 150f;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * launchVelocity;
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<VehicleWeaponItemLogic>().HackedEffect();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}       
