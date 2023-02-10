using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    Transform[] childArray;
    // Start is called before the first frame update
    void Start()
    {
        childArray = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
