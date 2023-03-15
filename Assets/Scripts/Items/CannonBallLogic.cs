using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLogic : MonoBehaviour
{
    [SerializeField] private float launchVelocity = 100f;
    public GameObject CannonballExplosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * launchVelocity;
        StartCoroutine("Explode");
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(Instantiate(CannonballExplosionParticle, other.transform.position, transform.rotation), 2);
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(5);
        Destroy(Instantiate(CannonballExplosionParticle, transform.position, transform.rotation), 2);
        Destroy(gameObject);
    }
}
