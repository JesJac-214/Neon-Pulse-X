using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public EquipmentBase[] items;
    private MeshRenderer[] meshRenderers;

    public AudioSource hit;
    public AudioSource restored;

    public enum GivenItem
    {
        All,
        SpeedBoost,
        Wall,
        Mine,
        Shield
    }

    public GivenItem spawnedItem;

    private void Start()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EquipmentBase[] items = { new SpeedBoost(), new Wall(), new Mine(), new Shield() };
   
        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            if (other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item.ammo == 0)
            {
                //The switch case below is for testing, if you want to remove useless code take away the switch case and uncomment the line below
                switch (spawnedItem)
                {
                    case GivenItem.All:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = items[Random.Range(0, items.Length)];
                        break;
                    case GivenItem.SpeedBoost:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new SpeedBoost();
                        break;
                    case GivenItem.Wall:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new Wall();
                        break;
                    case GivenItem.Mine:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new Mine();
                        break;
                    case GivenItem.Shield:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new Shield();
                        break;
                }
                //The switch case above is for testing, if you want to remove useless code take away the switch case and uncomment the line below
                //other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = items[Random.Range(0, items.Length)];
            }
            StartCoroutine("ItemDisappear");
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.enabled = false;
            }
            GetComponent<BoxCollider>().enabled = false;
            hit.Play();
        }
    }

    IEnumerator ItemDisappear()
    {
        yield return new WaitForSeconds(1);
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }
        GetComponent<BoxCollider>().enabled = true;
        restored.Play();
    }
}


