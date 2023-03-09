using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 7;
    [SerializeField] private float explosionForce = 100;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Vehicle Body"))
        {
             var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);
             foreach(var obj in surroundingObjects) 
            {
                if (obj.CompareTag("Vehicle Body"))
                {
                    Rigidbody rb = obj.transform.parent.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(explosionForce,transform.position, explosionRadius);
                }
                   
            }
             Destroy(gameObject);
        }    
    }
}
