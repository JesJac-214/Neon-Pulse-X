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
	public float accelerationSpeed = 1000.0f;
	public float decelerationEffectivity = 0.8f;
	public float frictionForce = 5f;
	public float maxSpeed = 30.0f;
	public float aimSpeed = 1500.0f;
	public int courseProgress = 0;
	public int playerID = 0;
	public Vector3 startPos;
	public int laps = 0;
	private bool grounded;

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
		if (grounded)
		{
			Rotate();
			Accelerate();
		}
		Aim();
		Debug.DrawRay(transform.position, vehicleRigidBody.velocity, Color.green);
	}

	private void Rotate()
    {
		vehicleRigidBody.transform.Rotate(0, rotationSpeed * steerInput * Time.deltaTime, 0);
		//anchor.transform.Rotate(0, rotationSpeed * steerInput * Time.deltaTime, 0);
	}

	private void Accelerate()
    {
		vehicleRigidBody.AddForce(accelerationSpeed * (accelerateInput - decelerationEffectivity * decelerateInput) * transform.forward * Time.deltaTime);
		if (!drift)
        {
			vehicleRigidBody.velocity -= Vector3.Project(vehicleRigidBody.velocity, vehicleRigidBody.transform.right) * frictionForce * Time.deltaTime;

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
		if (context.ReadValue<float>() == 1)
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
