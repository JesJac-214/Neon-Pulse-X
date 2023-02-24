using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle Body")
        {
            if (other.transform.parent.GetComponent<WeaponItemLogic>().Weapon.ammo == 0)
            {
                other.transform.parent.GetComponent<WeaponItemLogic>().Weapon = new CannonBall();
            }
            StartCoroutine("itemDisappear");
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator itemDisappear()
    {
        yield return new WaitForSeconds(1);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}
