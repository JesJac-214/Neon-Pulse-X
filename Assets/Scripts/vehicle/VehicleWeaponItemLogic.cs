using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleWeaponItemLogic : MonoBehaviour
{
	public GameObject anchor;

	public GameObject WallPrefab;
	public GameObject CannonBallPrefab;

	public GameObject gun;

	public EquipmentBase Weapon = new();
	public EquipmentBase Item = new();

	public float boostedSpeedValue = 30.0f;
	public float boostedAccelerationValue = 1000.0f;
	public float speedCoolDown = 1;

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

	}

		public void OnShoot(InputAction.CallbackContext context)
	{
		if (context.ReadValue<float>() == 0 && !gameObject.GetComponent<VehicleData>().gameManager.IsPaused)
		{
			Weapon.Use(gameObject);
		}
	}

	public void OnUseItem(InputAction.CallbackContext context)
	{
		if (context.ReadValue<float>() == 0 && !gameObject.GetComponent<VehicleData>().gameManager.IsPaused)
		{
			Item.Use(gameObject);
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
        Instantiate(WallPrefab, transform.position - transform.forward * 2, transform.rotation);
    }

	public void SpawnCannonBall()
	{
        Instantiate(CannonBallPrefab, transform.position + anchor.transform.forward * 5, anchor.transform.rotation);
    }

	public void Freeze()
	{

	}

	public void Invert()
	{

	}
}
