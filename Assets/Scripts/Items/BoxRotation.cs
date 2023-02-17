using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0, 0.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);
    }
}
