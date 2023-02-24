using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    //public EquipmentBase[] items = { new SpeedBoost(), new Wall() };
    private void OnTriggerEnter(Collider other)
    {
   
        if (other.gameObject.tag == "Vehicle Body")
        {
            if (other.transform.parent.GetComponent<WeaponItemLogic>().Item.ammo == 0)
            {
                switch(Random.Range(0, 2))
                {
                    case 0:
                        {
                            other.transform.parent.GetComponent<WeaponItemLogic>().Item = new Wall();
                        }
                        break;
                        case 1:
                        {
                            other.transform.parent.GetComponent<WeaponItemLogic>().Item = new SpeedBoost();
                        }
                        break;
                }
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


