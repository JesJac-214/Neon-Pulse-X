using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    //public GameObject box;
    public GameObject[] ItemEquipmentArray;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle Body")
        {
            if (other.transform.parent.GetComponent<vehicle>().itemAmmo == 0)
            {
                other.transform.parent.GetComponent<vehicle>().SecondaryPrefab = ItemEquipmentArray[Random.Range(0, ItemEquipmentArray.Length)];
                other.transform.parent.GetComponent<vehicle>().itemAmmo = 3;
            }
            //StartCoroutine("itemDisappear");
            //box.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    //IEnumerator itemDisappear()
    //{
    //    yield return new WaitForSeconds(5);
    //    box.GetComponent<MeshRenderer>().enabled = true;
    //}
}


