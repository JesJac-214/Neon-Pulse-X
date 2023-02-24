using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public EquipmentBase[] items;

    private void OnTriggerEnter(Collider other)
    {
        EquipmentBase[] items = { new SpeedBoost(), new Wall() };
   
        if (other.gameObject.tag == "Vehicle Body")
        {
            if (other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item.ammo == 0)
            {
                other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = items[Random.Range(0, items.Length)];
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


