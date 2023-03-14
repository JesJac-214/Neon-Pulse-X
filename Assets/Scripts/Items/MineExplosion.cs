using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 7;
    [SerializeField] private float explosionForce = 100;
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
                    rb.AddExplosionForce(explosionForce,transform.position, explosionRadius);
                }
                   
            }
            SpawnSphere();
             Destroy(gameObject);
        }    
    }
    private void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = transform.position;
        sphere.transform.localScale = new Vector3(1, 1, 1) * explosionRadius * 2;
        sphere.GetComponent<SphereCollider>().enabled = false;
        Destroy(sphere, 0.3f);
    }
}
