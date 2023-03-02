using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public EquipmentBase[] items;

    private void OnTriggerEnter(Collider other)
    {
        EquipmentBase[] items = { new SpeedBoost(), new Wall(), new Mine() };
   
        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            if (other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item.ammo == 0)
            {
                other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = items[Random.Range(0, items.Length)];
            }
            StartCoroutine("ItemDisappear");
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
    IEnumerator ItemDisappear()
    {
        yield return new WaitForSeconds(1);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}


