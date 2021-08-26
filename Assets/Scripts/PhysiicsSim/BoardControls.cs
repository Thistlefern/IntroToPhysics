using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControls : MonoBehaviour
{
    public Rigidbody board;
    Vector3 offset;
    public int speed;

    void Start()
    {
        offset = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            offset.z += speed * Time.deltaTime;
            if(offset.z > 50)
            {
                offset.z = 50;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            offset.z -= speed * Time.deltaTime;
            if (offset.z < -50)
            {
                offset.z = -50;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            offset.x += speed * Time.deltaTime;
            if (offset.x > 50)
            {
                offset.x = 50;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            offset.x -= speed * Time.deltaTime;
            if (offset.x < -50)
            {
                offset.x = -50;
            }
        }

        board.rotation = Quaternion.Euler(offset);
    }
}
