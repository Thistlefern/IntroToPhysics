using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public Rigidbody rbody;
    public int jumpHeight;

    bool hasJumped;
    bool isFalling;

    void Start()
    {
        hasJumped = false;
        isFalling = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && !hasJumped)
        {
            rbody.AddForce(transform.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
            hasJumped = true;
        }

        if(rbody.velocity.y < 0)
        {
            isFalling = true;
        }

        if(rbody.velocity.y == 0 && isFalling)
        {
            hasJumped = false;
            isFalling = false;
        }
    }
}