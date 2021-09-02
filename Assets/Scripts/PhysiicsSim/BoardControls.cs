using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControls : MonoBehaviour
{
    public Rigidbody board;
    Vector3 offset;
    public int speed;
    public int rotationMax;

    void Start()
    {
        offset = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            offset.z += speed * Time.deltaTime;
            if(offset.z > rotationMax)
            {
                offset.z = rotationMax;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            offset.z -= speed * Time.deltaTime;
            if (offset.z < 0 - rotationMax)
            {
                offset.z = 0 - rotationMax;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            offset.x += speed * Time.deltaTime;
            if (offset.x > rotationMax)
            {
                offset.x = rotationMax;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            offset.x -= speed * Time.deltaTime;
            if (offset.x < 0 - rotationMax)
            {
                offset.x = -0 - rotationMax;
            }
        }

        board.rotation = Quaternion.Euler(offset);
    }
}
