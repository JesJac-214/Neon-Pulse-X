using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPLogic : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 15;
    [SerializeField] private float launchVelocity = 100f;
    private float timer = 3;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * launchVelocity;
        StartCoroutine("DelayedExplosion");
    }
    IEnumerator DelayedExplosion()
    {
        yield return new WaitForSeconds(timer);
        var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var obj in surroundingObjects)
        {
             if (obj.CompareTag("Vehicle Body"))
             {
                  obj.transform.parent.GetComponent<VehicleWeaponItemLogic>().EMPEffect();
             }
        }
        SpawnSphere();
        Destroy(gameObject);
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
