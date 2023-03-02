using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPLogic : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 15;
    [SerializeField] private float launchVelocity = 10f;
    // Start is called before the first frame update
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
                    obj.transform.parent.GetComponent<VehicleWeaponItemLogic>().EMPEffect();
                }

            }
            Destroy(gameObject);
        }
    }
}
