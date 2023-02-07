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
	public float driftCompensation = 100.0f;
	public float maxSpeed = 30.0f;
	public float aimSpeed = 1500.0f;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		controller = GetComponent<CharacterController>();
		playerControls = new PlayerControls();
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
		rotate();
		accelerate();
		aim();
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

	private void rotate()
    {
		vehicleRigidBody.transform.Rotate(0, rotationSpeed * steerInput * Time.deltaTime, 0);
		anchor.transform.Rotate(0, -rotationSpeed * steerInput * Time.deltaTime, 0);
	}

	private void accelerate()
    {
		vehicleRigidBody.AddForce(accelerationSpeed * (accelerateInput - decelerationEffectivity * decelerateInput) * transform.forward * Time.deltaTime);
		//vehicleRigidBody.AddForce(Vector3.Project(vehicleRigidBody.velocity, vehicleRigidBody.transform.right).magnitude * vehicleRigidBody.transform.forward * Time.deltaTime);
		if (!drift)
        {
			vehicleRigidBody.AddForce(-Vector3.Project(vehicleRigidBody.velocity, vehicleRigidBody.transform.right) * driftCompensation * Time.deltaTime);
        }
		vehicleRigidBody.velocity = Vector3.ClampMagnitude(vehicleRigidBody.velocity, maxSpeed);
	}

	private void aim()
    {
		if (isGamepad)
		{
			if (aimInput.x != 0 && aimInput.y != 0)
			{
				Vector3 direction = Vector3.right * aimInput.x + Vector3.forward * aimInput.y;
				anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
			}
		}
		else
		{
			Vector3 direction = Vector3.right * aimInput.x + Vector3.forward * aimInput.y;
			if (direction != Vector3.zero)
			{
				anchor.transform.rotation = Quaternion.RotateTowards(anchor.transform.rotation, Quaternion.LookRotation(direction), aimSpeed * Time.deltaTime);
			}
		}
	}

	public void OnDeviceChange(PlayerInput pi)
	{
		isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
	}

	public void OnRespawn(InputAction.CallbackContext context)
    {
		if (context.ReadValue<float>() == 1)
        {
			vehicleRigidBody.velocity = Vector3.zero;
			transform.SetPositionAndRotation(new Vector3(0, 3, -20), Quaternion.Euler(0, -90, 0));
			anchor.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
	}

	public void OnQuitGame()
	{
		Application.Quit();
	}

	public void OnSwitchTrack()
    {
		SceneManager.LoadScene("Track2");
	}
}
