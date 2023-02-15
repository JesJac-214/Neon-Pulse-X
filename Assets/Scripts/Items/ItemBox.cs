using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject[] ItemEquipmentArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle Body")
        {
            other.transform.parent.GetComponent<vehicle>().SecondaryPrefab = ItemEquipmentArray[Random.Range(0,ItemEquipmentArray.Length)];
            other.transform.parent.GetComponent<vehicle>().itemAmmo = 3;
        }
            Debug.Log("Box hit");
    }
}
