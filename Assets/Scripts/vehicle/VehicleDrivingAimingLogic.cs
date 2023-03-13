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

	//private bool isGamepad;
	private bool drift = false;

	private float steerInput = 0;
	private float accelerateInput = 0;
	private float decelerateInput = 0;
	//private Vector2 aimInput = Vector2.zero;

	//public float rotationSpeed = 120.0f;
	public float accelerationSpeed = 1000.0f;
	public float decelerationEffectivity = 0.8f;
	//public float frictionForce = 5f;
	public float maxSpeed = 30.0f;
	//public float aimSpeed = 1500.0f;
	private bool grounded;

	public bool canAccel = true;
	public bool hasFriction = true;

	[SerializeField]
	public float springStrength = 10000.0f;
	[SerializeField]
	public float springDamper = 15.0f;
	[SerializeField]
	public float tireRotationSmoothing = 5.0f;
	[SerializeField]
	public float tireTiltAngle = 30f;

	[SerializeField]
	private float friction = 1f;
	[SerializeField]
	private float driftFriction = 0.2f;
	[SerializeField]
	private float rollingFriction = 0.2f;
	[SerializeField]
	private float backTireFrictionMultiplier = 2f;

	public GameObject[] tires;
	public GameObject[] frontTires;
	public GameObject[] backTires;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		controller = GetComponent<CharacterController>();
		playerControls = new PlayerControls();
	}

	private void Start()
	{
		transform.SetPositionAndRotation(GetComponent<VehicleData>().startTransform.position, GetComponent<VehicleData>().startTransform.rotation);
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
		//HandleUprightForce();
		if (grounded)
		{
			if (canAccel)
			{
				HandleTireAcceleration();
			}
			if(hasFriction)
			{
				HandleTireFriction();
			}
		}
		if (!grounded)
        {
			HandleUprightForce();
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
		foreach (GameObject tire in frontTires)
        {
			vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.right) * rollingFriction * Time.fixedDeltaTime, tire.transform.position, ForceMode.VelocityChange);
			if (!drift)
			{
				vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward) * Time.fixedDeltaTime * friction, tire.transform.position, ForceMode.VelocityChange);
			}
			else
			{
				vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward) * Time.fixedDeltaTime * driftFriction, tire.transform.position, ForceMode.VelocityChange);
			}
		}

		foreach (GameObject tire in backTires)
        {
			if (!drift)
			{
				vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward) * Time.fixedDeltaTime * friction * backTireFrictionMultiplier, tire.transform.position, ForceMode.VelocityChange);
			}
			else
			{
				vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.forward) * Time.fixedDeltaTime * driftFriction, tire.transform.position, ForceMode.VelocityChange);
			}
			vehicleRigidBody.AddForceAtPosition(-Vector3.Project(vehicleRigidBody.GetPointVelocity(tire.transform.position), tire.transform.right) * rollingFriction * Time.fixedDeltaTime, tire.transform.position, ForceMode.VelocityChange);
		}
	}
	public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
	{
		if (Quaternion.Dot(a, b) < 0)
		{
			return a * Quaternion.Inverse(Multiply(b, -1));
		}
		else return a * Quaternion.Inverse(b);
	}
	public static Quaternion Multiply(Quaternion input, float scalar)
	{
		return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
	}

	private void HandleUprightForce()
	{
		Quaternion vehicleCurrent = transform.rotation;
		//Quaternion toGoal = transform.rotation * new Quaternion(0,/*-0.707106829f*/0,0,0.707106829f) * Quaternion.Inverse(vehicleCurrent);
		Quaternion toGoal = ShortestRotation(transform.rotation * new Quaternion(0, 0, 0, 0.707106829f), vehicleCurrent);

		Vector3 rotAxis;
		float rotDegrees;

		toGoal.ToAngleAxis(out rotDegrees, out rotAxis);
		rotAxis.Normalize();

		float rotRadians = rotDegrees * Mathf.Deg2Rad;

		vehicleRigidBody.AddTorque((rotAxis * (rotRadians * 200)) - (vehicleRigidBody.angularVelocity * 20));
	}

	private void Aim()
	{
		anchor.transform.position = transform.position + new Vector3(0, 4f, 0);
		//Vector3 direction = Vector3.right * aimInput.x + Vector3.forward * aimInput.y;
		//if (isGamepad)
		//{
		//	if (aimInput.x != 0 && aimInput.y != 0)
		//	{
		//		anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
		//	}
		//}
		//else
		//{
		//	if (direction != Vector3.zero)
		//	{
		//		anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
		//	}
		//}
	}

	public void OnSteer(InputAction.CallbackContext context)
	{
		steerInput = context.ReadValue<float>();
	}

	public void OnAim(InputAction.CallbackContext context)
	{
		//aimInput = context.ReadValue<Vector2>();
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
		if (context.started)
		{
			drift = true;
		}
		else if (context.canceled)
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
