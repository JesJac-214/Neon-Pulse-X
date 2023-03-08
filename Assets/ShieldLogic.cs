using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLogic : MonoBehaviour
{
    public float counterShieldTime = 3;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Projectile") || collision.CompareTag("Ice") || collision.CompareTag("Hackingdrone"))
        {
            Debug.Log(collision);
            Destroy(collision.gameObject);
        }
    }

    void Start()
    {
        transform.SetSiblingIndex(0);
        Destroy(gameObject, counterShieldTime);
    }
}
