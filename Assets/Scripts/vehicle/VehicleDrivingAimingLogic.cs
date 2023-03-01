using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleDrivingAimingLogic : MonoBehaviour
{
	private PlayerInput playerInput;
	private CharacterController controller;
	private PlayerControls playerControls;

	public Rigidbody vehicleRigidBody;
	public GameObject anchor;

	private bool isGamepad;
	private bool drift = false;

	private float steerInput = 0;
	private float accelerateInput = 0;
	private float decelerateInput = 0;
	private Vector2 aimInput = Vector2.zero;

	public float rotationSpeed = 120.0f;
	public float accelerationSpeed = 1000.0f;
	public float decelerationEffectivity = 0.8f;
	public float frictionForce = 5f;
	public float maxSpeed = 30.0f;
	public float aimSpeed = 1500.0f;
	private bool grounded;

	public GameObject[] tires;
	public GameObject[] frontTires;
	public GameObject[] backTires;

	[SerializeField]
	public float springStrength = 10000.0f;
	[SerializeField]
	public float springDamper = 15.0f;
	[SerializeField]
	public float tireRotationSmoothing = 5.0f;
	[SerializeField]
	private float tireTiltAngle = 30f;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		controller = GetComponent<CharacterController>();
		playerControls = new PlayerControls();
	}

	private void Start()
	{
		transform.position = transform.GetComponent<VehicleData>().startPos;
	}

	private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}

	private void FixedUpdate()
	{
		HandleTireSuspension();
		HandleTireRotation();
		if (grounded)
		{
			HandleTireAcceleration();
			HandleTireFriction();
		}
	}

	void Update()
	{
		
		Aim();
	}

	private void HandleTireSuspension()
	{
		grounded = false;
		foreach (GameObject tire in tires)
		{
			Ray ray = new Ray(tire.transform.position, -tire.transform.up);
			if (Physics.Raycast(ray, out RaycastHit hit, 4))
			{
				if (hit.collider.CompareTag("Track"))
				{
					float offset = 2f - hit.distance;
					Vector3 springDir = tire.transform.up;
					float vel = Vector3.Dot(springDir, vehicleRigidBody.GetPointVelocity(tire.transform.position));
					float force = (offset * springStrength) - (vel * springDamper);
					vehicleRigidBody.AddForceAtPosition(force * springDir * Time.fixedDeltaTime, tire.transform.position);
					Debug.DrawRay(tire.transform.position, 2*-springDir, Color.green);
					Debug.DrawRay(tire.transform.position, force * springDir, Color.cyan);
					Debug.Log("Offset: " + offset + "\nForce: " + force);
					grounded = true;
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
			Debug.DrawRay(tire.transform.position, 2 * -tire.transform.right, Color.blue);
		}
	}

	private void HandleTireAcceleration()
	{
		foreach (GameObject tire in backTires)
		{
			vehicleRigidBody.AddForceAtPosition(-tire.transform.right * (accelerateInput - decelerateInput * decelerationEffectivity) * accelerationSpeed * Time.fixedDeltaTime, tire.transform.position, ForceMode.VelocityChange);
			Debug.DrawRay(tire.transform.position, -tire.transform.right * (accelerateInput - decelerateInput * decelerationEffectivity) * accelerationSpeed, Color.white);
			vehicleRigidBody.velocity = Vector3.ClampMagnitude(vehicleRigidBody.velocity, maxSpeed);
		}
	}

	private void HandleTireFriction()
	{
		foreach (GameObject tire in tires)
		{
			if (!drift)
            {
				vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward) * Time.fixedDeltaTime, tire.transform.position, ForceMode.VelocityChange);
            }
			else
            {
				vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward) * Time.fixedDeltaTime * 0.2f, tire.transform.position, ForceMode.VelocityChange);
				Debug.Log("Drifting!");
			}
			vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.right) * 0.2f * Time.fixedDeltaTime, tire.transform.position, ForceMode.VelocityChange);
		}
		foreach (GameObject tire in frontTires)
		{
			vehicleRigidBody.AddForceAtPosition(Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward).magnitude * -tire.transform.right * 0.8f * Time.fixedDeltaTime, tire.transform.position, ForceMode.VelocityChange);
		}
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

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "Track")
		{
			grounded = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Track")
		{
			grounded = false;
		}
	}
}
