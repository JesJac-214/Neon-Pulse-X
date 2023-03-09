using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public EquipmentBase[] weapons;
    private MeshRenderer[] meshRenderers;

    public AudioSource hit;
    public AudioSource restored;

    private void Start()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EquipmentBase[] weapons = { new CannonBall(), new IceBeam(), new EMP(), new HackingDevice(), new SoundWave() };

        if (other.gameObject.CompareTag("Vehicle Body"))
        {
            if (other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item.ammo == 0)
            {
                other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = weapons[Random.Range(0, weapons.Length)];
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
