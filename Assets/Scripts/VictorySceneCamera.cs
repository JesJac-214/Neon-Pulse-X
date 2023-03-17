using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySceneCamera : MonoBehaviour
{
    public Transform[] spawns;
    public Vector3 cameraOffset = new(0, 10, 100);
    private Vector3 goalPos;
    private Vector3 positionVelocity = Vector3.zero;
    private float cameraMoveLag = 20f;

    public GameObject buttons;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(CameraSweep));
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref positionVelocity, cameraMoveLag * Time.smoothDeltaTime);
    }

    IEnumerator CameraSweep()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        for (int i = spawns.Length - 1; i >= 0; i--)
        {
            foreach (GameObject vehicle in vehicles)
            {
                if (vehicle.GetComponent<VehicleData>().placement == i)
                {
                    goalPos = spawns[i].position + cameraOffset;
                    yield return new WaitForSeconds(1);
                }
            }
        }
        goalPos = new Vector3(0, 24.8999996f, -49.5999985f) * 0.5f;
        buttons.SetActive(true);
    }
}
