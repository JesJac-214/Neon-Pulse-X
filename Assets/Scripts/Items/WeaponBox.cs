using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public EquipmentBase[] weapons;

    private void OnTriggerEnter(Collider other)
    {
        EquipmentBase[] weapons = { new CannonBall() };
        if (other.gameObject.tag == "Vehicle Body")
        {
            if (other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Weapon.ammo == 0)
            {
                other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Weapon = weapons[Random.Range(0, weapons.Length)];
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
