using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public EquipmentBase[] weapons;
    private MeshRenderer[] meshRenderers;

    public AudioSource hit;
    public AudioSource restored;

    public enum GivenWeapon
    {
        All,
        CannonBall,
        IceBeam,
        EMP,
        HackingDevice,
        SoundWave
    }

    public GivenWeapon spawnedWeapon;

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
                //The switch case below is for testing, if you want to remove useless code take away the switch case and uncomment the line below
                switch (spawnedWeapon)
                {
                    case GivenWeapon.All:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = weapons[Random.Range(0, weapons.Length)];
                        break;
                    case GivenWeapon.CannonBall:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new CannonBall();
                        break;
                    case GivenWeapon.IceBeam:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new IceBeam();
                        break;
                    case GivenWeapon.EMP:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new EMP();
                        break;
                    case GivenWeapon.HackingDevice:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new HackingDevice();
                        break;
                    case GivenWeapon.SoundWave:
                        other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = new SoundWave();
                        break;
                }
                //The switch case above is for testing, if you want to remove useless code take away the switch case and uncomment the line below
                //other.transform.parent.GetComponent<VehicleWeaponItemLogic>().Item = weapons[Random.Range(0, weapons.Length)];
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
