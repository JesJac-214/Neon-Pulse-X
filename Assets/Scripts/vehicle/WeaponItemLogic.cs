using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponItemLogic : MonoBehaviour
{
	public GameObject anchor;
	
	public GameObject PrimaryPrefab;
	public GameObject SecondaryPrefab;

	public GameObject gun;

	public int weaponAmmo;
	public int itemAmmo;

	public float boostedSpeedValue = 30.0f;
	public float boostedAccelerationValue = 1000.0f;
	public float speedCoolDown = 1;

	void Update()
	{
		if (weaponAmmo == 0)
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
		if (context.ReadValue<float>() == 0 && weaponAmmo > 0)
		{
			Instantiate(PrimaryPrefab, transform.position + anchor.transform.forward * 2, anchor.transform.rotation);
			weaponAmmo--;
		}
	}

	public void OnUseItem(InputAction.CallbackContext context)
	{
		if (context.ReadValue<float>() == 0 && itemAmmo > 0)
		{
			Instantiate(SecondaryPrefab, transform.position - transform.forward * 2, transform.rotation);
			itemAmmo--;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "SpeedBoost")
		{
			transform.GetComponent<DrivingAimingLogic>().maxSpeed += boostedSpeedValue;
			transform.GetComponent<DrivingAimingLogic>().accelerationSpeed += boostedAccelerationValue;
			StartCoroutine("SpeedDuration");
			Debug.Log("Vehicle boost");
		}

	}

	IEnumerator SpeedDuration()
	{
		yield return new WaitForSeconds(speedCoolDown);
		transform.GetComponent<DrivingAimingLogic>().maxSpeed -= boostedSpeedValue;
		transform.GetComponent<DrivingAimingLogic>().accelerationSpeed -= boostedAccelerationValue;
		Debug.Log("slowdown");
	}
}
