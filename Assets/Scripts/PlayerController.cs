using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * 5 * Time.deltaTime;
        }
    }
}