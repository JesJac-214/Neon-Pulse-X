using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public EquipmentBase[] weapons;
    private MeshRenderer[] meshRenderers;

    private void Start()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EquipmentBase[] weapons = { /*new CannonBall(), new IceBeam(), new EMP(),*/ new HackingDevice() };

        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            if (other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Weapon.ammo == 0)
            {
                other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Weapon = weapons[Random.Range(0, weapons.Length)];
            }
            StartCoroutine("ItemDisappear");
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.enabled = false;
            }
            GetComponent<BoxCollider>().enabled = false;
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
    }
}
