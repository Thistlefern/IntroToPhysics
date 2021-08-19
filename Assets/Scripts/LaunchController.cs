using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
    public SimpleRigidbody[] launchRigidbodies = new SimpleRigidbody[2];

    void Start()
    {
        launchRigidbodies[0].velocity.y = 5;
        launchRigidbodies[1].velocity.y = 5;

    }

    void Update()
    {
        
    }
}
