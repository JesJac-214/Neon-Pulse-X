using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingLogic : MonoBehaviour
{
    [SerializeField] private float launchVelocity = 100f;
    [SerializeField] private float explosionRadius = 2;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * launchVelocity;
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var obj in surroundingObjects)
            {
                if (obj.CompareTag("Vehicle Body"))
                {
                    obj.transform.parent.GetComponent<VehicleWeaponItemLogic>().HackedEffect();
                }

            }
            Destroy(gameObject);
        }
    }
}       
