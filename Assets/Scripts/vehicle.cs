using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class vehicle : MonoBehaviour
{
	private PlayerInput playerInput;
	private CharacterController controller;
	private PlayerControls playerControls;

	public Camera cam;
	public Rigidbody vehicleRigidBody;
	public GameObject anchor;
	public GameObject projectilePrefab;
	public GameObject obstaclePrefab;

	private bool isGamepad;
	private bool drift = false;

	private float steerInput = 0;
	private float accelerateInput = 0;
	private float decelerateInput = 0;
	private Vector2 aimInput = Vector2.zero;

	public float rotationSpeed = 120.0f;
	public float accelerationSpeed = 1000000.0f;
	public float decelerationEffectivity = 0.8f;
	public float frictionForce = 5f;
	public float maxSpeed = 30.0f;
	public float aimSpeed = 1500.0f;
	public int courseProgress = 0;
	public int playerID = 0;
	public Vector3 startPos;
	public int laps = 0;
	private bool grounded;

	public int totalWeaponAmmo = 5;
	public int totalItemAmmo = 3;
	private int weaponAmmo = 5;
	private int itemAmmo = 3;

	public GameObject[] tires;
	public GameObject[] frontTires;
	private float springStrength = 100.0f;
	private float springDamper = 30.0f;
	public float tireRotationSmoothing = 5.0f;
	public float tireTiltAngle = 60.0f;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		controller = GetComponent<CharacterController>();
		playerControls = new PlayerControls();
	}

    private void Start()
    {
		transform.position = startPos;
    }

    private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}


    void Update()
    {
		HandleTireSuspension();
		if (grounded)
		{
			Rotate();
			//HandleTireRotation();
			//Accelerate();
			HandleTireAcceleration();
		}
		Aim();
	}

	private void Rotate()
    {
		vehicleRigidBody.transform.Rotate(0, rotationSpeed * steerInput * Time.deltaTime, 0);
		//anchor.transform.Rotate(0, rotationSpeed * steerInput * Time.deltaTime, 0);
	}

	private void Accelerate()
    {
		vehicleRigidBody.AddForce((accelerateInput - decelerationEffectivity * decelerateInput) * accelerationSpeed * Time.deltaTime * transform.forward);
		if (!drift)
        {
			vehicleRigidBody.velocity -= frictionForce * Time.deltaTime * Vector3.Project(vehicleRigidBody.velocity, vehicleRigidBody.transform.right);

		}
		vehicleRigidBody.velocity = Vector3.ClampMagnitude(vehicleRigidBody.velocity, maxSpeed);
	}

	private void Aim()
    {
		anchor.transform.position = transform.position + new Vector3(0, 1f, 0);
		Vector3 direction = Vector3.right * aimInput.x + Vector3.forward * aimInput.y;
		if (isGamepad)
		{
			if (aimInput.x != 0 && aimInput.y != 0)
			{
				anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
			}
		}
		else
		{
			if (direction != Vector3.zero)
			{
				anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
			}
		}
	}

	private void HandleTireSuspension()
    {
		foreach (GameObject tire in tires)
        {
			Ray ray = new Ray(tire.transform.position, -tire.transform.up);
			if (Physics.Raycast(ray, out RaycastHit hit, 2))
            {
				float offset = 1f - hit.distance;
				Vector3 springDir = tire.transform.up;
				float vel = Vector3.Dot(springDir, tire.GetComponent<wheels>().velocity);
				float force = (offset * springStrength) - (vel * springDamper);
				vehicleRigidBody.AddForceAtPosition(springDir * force, tire.transform.position);
				grounded = true;
            }
			else
            {
				grounded = false;
            }
			Debug.DrawRay(tire.transform.position, 2 * -tire.transform.up, Color.green);
        }
    }

	private void HandleTireRotation()
    {
		foreach (GameObject tire in frontTires)
        {
			
			Quaternion target = Quaternion.Euler(0, steerInput * tireTiltAngle, 0);
			tire.transform.rotation = Quaternion.Slerp(tire.transform.rotation, target, Time.deltaTime * tireRotationSmoothing);;
			Debug.DrawRay(tire.transform.position, 2 * -tire.transform.right, Color.blue);
        }
    }

	private void HandleTireAcceleration()
    {
		foreach (GameObject tire in frontTires)
        {
			vehicleRigidBody.AddForceAtPosition(-tire.transform.right * accelerateInput * accelerationSpeed * Time.deltaTime, tire.transform.position);
        }
    }

	public void OnSteer(InputAction.CallbackContext context)
    {
		steerInput = context.ReadValue<float>();
	}

	public void OnAim(InputAction.CallbackContext context)
	{
		aimInput = context.ReadValue<Vector2>();
	}

	public void OnAccelerate(InputAction.CallbackContext context)
	{
		accelerateInput = context.ReadValue<float>();
	}

	public void OnDecelerate(InputAction.CallbackContext context)
	{
		decelerateInput = context.ReadValue<float>();
	}

	public void OnShoot(InputAction.CallbackContext context)
    {
		if (context.ReadValue<float>() == 0 && weaponAmmo > 0)
        {
			Instantiate(projectilePrefab, transform.position + anchor.transform.forward * 2, anchor.transform.rotation);
			weaponAmmo--;
        }
    }

	public void OnUseItem(InputAction.CallbackContext context)
    {
		if (context.ReadValue<float>() == 0 && itemAmmo > 0)
        {
			Instantiate(obstaclePrefab, transform.position + anchor.transform.forward * 2, anchor.transform.rotation);
			itemAmmo--;
        }
    }

	public void OnDrift(InputAction.CallbackContext context)
    {
		if (context.ReadValue<float>() == 1)
        {
			drift = true;
        }
		else
        {
			drift = false;
        }
    }

	public void OnRespawn(InputAction.CallbackContext context)
    {
		if (context.ReadValue<float>() == 1)
        {
			vehicleRigidBody.velocity = Vector3.zero;
			transform.SetPositionAndRotation(startPos, Quaternion.Euler(0, -90, 0));
			anchor.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
	}

	public void OnQuitGame()
	{
		Application.Quit();
	}

	public void IncrementProgress()
    {
		courseProgress++;
    }

	public void DecrementProgress()
	{
		courseProgress--;
	}

	public void ResetAmmo()
    {
		weaponAmmo = totalWeaponAmmo;
		itemAmmo = totalItemAmmo;
    }

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "Track")
		{
			//grounded = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Track")
		{
			//grounded = false;
		}
	}
}
