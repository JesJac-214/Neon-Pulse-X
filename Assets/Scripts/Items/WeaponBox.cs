using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public GameObject[] weaponEquipmentArray;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle Body")
        {
            if (other.transform.parent.GetComponent<WeaponItemLogic>().weaponAmmo == 0)
            {
                other.transform.parent.GetComponent<WeaponItemLogic>().PrimaryPrefab = weaponEquipmentArray[Random.Range(0, weaponEquipmentArray.Length)];
                other.transform.parent.GetComponent<WeaponItemLogic>().weaponAmmo = 10;
            }
        }
        StartCoroutine("itemDisappear");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator itemDisappear()
    {
        yield return new WaitForSeconds(5);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}
