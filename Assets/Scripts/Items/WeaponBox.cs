using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public GameObject[] weaponEquipmentArray;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle Body" && other.transform.parent.GetComponent<vehicle>().weaponAmmo == 0)
        {
            other.transform.parent.GetComponent<vehicle>().PrimaryPrefab = weaponEquipmentArray[Random.Range(0,weaponEquipmentArray.Length)];
            other.transform.parent.GetComponent<vehicle>().weaponAmmo = 10;
        }
    }
}
