using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagement : MonoBehaviour
{
    public GameObject target;
    public Camera cam;
    
    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach(var plane in planes)
        {
            if(plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Player");
        if (vehicles.Length > 0)
        {
            foreach (GameObject vehicle in vehicles)
            {
                if (!IsVisible(cam, vehicle))
                {
                    vehicle.transform.position = cam.transform.position - new Vector3(0, 50, -20);
                    Debug.Log(vehicle.GetComponent<vehicle>().playerID);
                }
            }
        }
    }
}
