using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 7;
    [SerializeField] private float explosionForce = 150;
    public GameObject explosionEffect;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
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
                    if (!obj.transform.parent.GetComponent<VehicleData>().isShielded)
                    {
                        rb.AddExplosionForce(explosionForce,transform.position, explosionRadius);
                    }
                }
            }
            Explode();
            //SpawnSphere();
        }    
    }
    private void Explode()
    {
        Destroy(Instantiate(explosionEffect, transform.position, transform.rotation), 2);
        Destroy(gameObject);
    }
    private void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = transform.position;
        sphere.transform.localScale = new Vector3(1, 1, 1) * explosionRadius * 2;
        sphere.GetComponent<SphereCollider>().enabled = false;
        Destroy(sphere, 0.3f);
        Destroy(gameObject);
    }
}