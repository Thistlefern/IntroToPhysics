using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTimestep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 200;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime);
        Debug.Log("Update");
    }

    void FixedUpdate()
    {
        //Debug.Log(Time.deltaTime);
        Debug.Log("Fixed Update");
    }
}
