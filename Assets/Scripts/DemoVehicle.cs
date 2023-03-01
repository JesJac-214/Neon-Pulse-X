using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DemoVehicle : MonoBehaviour
{
	public enum DemoMode
	{
    	Acceleration,
    	Steering,
		Still
	}

	public DemoMode mode;

	public Rigidbody vehicleRigidBody;
	public GameObject anchor;
	public GameObject projectilePrefab;
	public GameObject obstaclePrefab;

	private bool drift = false;

	private float steerInput = 0;
	private float accelerateInput = 0;
	private float decelerateInput = 0;
	private Vector2 aimInput = Vector2.zero;

	public float rotationSpeed = 120.0f;
	[SerializeField]
	private float accelerationSpeed = 10.0f;
	public float decelerationEffectivity = 0.8f;
	public float frictionForce = 5f;
	public float maxSpeed = 30.0f;
	public float aimSpeed = 1500.0f;
	public int courseProgress = 0;
	public int playerID = 0;
	public Vector3 startPos;
	public int laps = 0;
	private bool grounded;

	public GameObject[] tires;
	public GameObject[] frontTires;
	public GameObject[] backTires;

	public float springStrength = 100.0f;
	public float springDamper = 15.0f;
	public float tireRotationSmoothing = 5.0f;
	[SerializeField]
	private float tireTiltAngle = 30f;
	public float tireMass = 5.0f;
	public float tireGripFactor = 1.0f;


    private void Start()
    {
		if (mode == DemoMode.Acceleration)
		{
			accelerateInput = 1;
		}
		if (mode == DemoMode.Steering)
		{
			accelerateInput = 1;
		}
		startPos = transform.position;
    }


    void Update()
    {
		if (transform.position.x < -25 && mode == DemoMode.Acceleration)
		{
			Destroy(gameObject.transform.parent.gameObject);
		}
		if (mode == DemoMode.Steering)
		{
			steerInput = Mathf.Sin(Time.time);
			if (!grounded)
			{
				transform.SetPositionAndRotation(startPos, Quaternion.Euler(0, -90, 0));
			}
		}
		HandleTireSuspension();
		HandleTireRotation();
		if (grounded)
		{
			HandleTireAcceleration();
			HandleTireFriction();
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
		// if (isGamepad)
		// {
		// 	if (aimInput.x != 0 && aimInput.y != 0)
		// 	{
		// 		anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
		// 	}
		// }
		// else
		// {
		// 	if (direction != Vector3.zero)
		// 	{
		// 		anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
		// 	}
		// }
	}

	private void HandleTireSuspension()
    {
		foreach (GameObject tire in tires)
        {
			tire.transform.GetChild(0).position = tire.transform.position - new Vector3(0, 1f, 0);
			Ray ray = new Ray(tire.transform.position, -tire.transform.up);
			if (Physics.Raycast(ray, out RaycastHit hit, 2))
            {
				if (hit.collider.CompareTag("Track"))
                {
					float offset = 1f - hit.distance;
					Vector3 springDir = tire.transform.up;
					float vel = Vector3.Dot(springDir, vehicleRigidBody.GetPointVelocity(tire.transform.position));
					float force = (offset * springStrength) - (vel * springDamper);
					vehicleRigidBody.AddForceAtPosition(springDir * force, tire.transform.position);
					if (hit.distance < 1.5f)
					{
						tire.transform.GetChild(0).position = hit.point + new Vector3(0, 0.5f, 0);
					}
					grounded = true;
                }
				else
			    {
					grounded = false;
	            }
            }
        }
    }

	private void HandleTireRotation()
    {
		foreach (GameObject tire in frontTires)
        {
            Quaternion target = Quaternion.Euler(0, steerInput * tireTiltAngle, 0) * vehicleRigidBody.rotation * Quaternion.Euler(0, 90, 0);
			tire.transform.rotation = target;
        }
    }

	private void HandleTireAcceleration()
    {
		foreach (GameObject tire in backTires)
        {
			vehicleRigidBody.AddForceAtPosition(-tire.transform.right * (accelerateInput - decelerateInput * decelerationEffectivity) * accelerationSpeed * Time.deltaTime, tire.transform.position, ForceMode.VelocityChange);
        }
    }

	private void HandleTireFriction()
	{
		foreach (GameObject tire in tires)
		{
			vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward) * Time.deltaTime, tire.transform.position, ForceMode.VelocityChange);
			vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.right) * 0.2f * Time.deltaTime, tire.transform.position, ForceMode.VelocityChange);
		}
		foreach (GameObject tire in frontTires)
        {
			vehicleRigidBody.AddForceAtPosition(Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward).magnitude * -tire.transform.right * 0.8f * Time.deltaTime, tire.transform.position, ForceMode.VelocityChange);
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
		if (context.ReadValue<float>() == 0)
        {
			Instantiate(projectilePrefab, transform.position + anchor.transform.forward * 2, anchor.transform.rotation);
        }
    }

	public void OnUseItem(InputAction.CallbackContext context)
    {
		if (context.ReadValue<float>() == 0)
        {
			Instantiate(obstaclePrefab, transform.position + anchor.transform.forward * 2, anchor.transform.rotation);
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
}
