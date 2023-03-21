using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLogic : MonoBehaviour
{
    public float destructionDelay = 0.2f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject, destructionDelay);
        }
    }
}
