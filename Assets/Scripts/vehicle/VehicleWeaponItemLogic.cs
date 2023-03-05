using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VehicleWeaponItemLogic : MonoBehaviour
{
	public GameObject anchor;

	public GameObject WallPrefab;
	public GameObject CannonBallPrefab;
	public GameObject IcebeamPrefab;
	public GameObject MinePrefab;
	public GameObject EMPPrefab;

    public GameObject gun;

	public EquipmentBase Weapon = new();
	public EquipmentBase Item = new();

	public float boostedSpeedValue = 30.0f;
	public float boostedAccelerationValue = 1000.0f;
	public float speedCoolDown = 1;
	public float frozenfriction = 5;
	public float frozenCoolDown = 4;
	public float EMPCoolDown = 5; 

    void Update()
	{
		if (Weapon.ammo == 0)
		{
			gun.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			gun.GetComponent<MeshRenderer>().enabled = true;
		}
		if (SceneManager.GetActiveScene().name == "PlayerJoin" || SceneManager.GetActiveScene().name == "VictoryPodium")
        {
			gun.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public void OnShoot(InputAction.CallbackContext context)
	{
		if (context.started)
        {
			if (GetComponent<VehicleData>().gameManager != null)
            {
				if (!GetComponent<VehicleData>().gameManager.IsPaused)
                {
					Weapon.Use(gameObject);
                }
            }
			else 
            {
				SpawnCannonBall();
            }
        }
    }

	public void OnUseItem(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			if (GetComponent<VehicleData>().gameManager != null)
			{
				if (!GetComponent<VehicleData>().gameManager.IsPaused)
				{
					Item.Use(gameObject);
				}
			}
			else
			{
                SpawnWall();
			}
		}
	}

	public void SpeedBoost()
	{
		transform.GetComponent<VehicleDrivingAimingLogic>().maxSpeed += boostedSpeedValue;
        transform.GetComponent<VehicleDrivingAimingLogic>().accelerationSpeed += boostedAccelerationValue;
        StartCoroutine("SpeedDuration");
	}
	
	IEnumerator SpeedDuration()
	{
		yield return new WaitForSeconds(speedCoolDown);
		transform.GetComponent<VehicleDrivingAimingLogic>().maxSpeed -= boostedSpeedValue;
		transform.GetComponent<VehicleDrivingAimingLogic>().accelerationSpeed -= boostedAccelerationValue;
	}
	
	public void SpawnWall()
	{
        Instantiate(WallPrefab, transform.position - transform.forward * 3, transform.rotation);
    }

    public void SpawnMine()
    {
        Instantiate(MinePrefab, transform.position - transform.forward * 3, transform.rotation);
    }

    public void SpawnCannonBall()
	{
        Instantiate(CannonBallPrefab, transform.position + anchor.transform.forward * 5, anchor.transform.rotation);
    }

    public void SpawnEMP()
    {
        Instantiate(EMPPrefab, transform.position + anchor.transform.forward * 3, anchor.transform.rotation);
    }

    public void SpawnIcebeamBullet()
    {
        Instantiate(IcebeamPrefab, transform.position + anchor.transform.forward * 5, anchor.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ice"))
		{
			Freeze();
		}
    }
    public void Freeze()
	{
        GetComponent<VehicleDrivingAimingLogic>().hasFriction = false;
        StartCoroutine("FrozenDuration");
    }
    IEnumerator FrozenDuration()
	{
		yield return new WaitForSeconds(frozenCoolDown);
        GetComponent<VehicleDrivingAimingLogic>().hasFriction = true;
    }

    public void EMPEffect()
	{
		GetComponent<VehicleDrivingAimingLogic>().canAccel = false;
        StartCoroutine("EMPDuration");
    }
    IEnumerator EMPDuration()
    {
        yield return new WaitForSeconds(EMPCoolDown);
        GetComponent<VehicleDrivingAimingLogic>().canAccel = true;
    }
}
