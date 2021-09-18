using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicCharacter : MonoBehaviour
{
    public Rigidbody rbody;
    public Vector3 velocity;
    public float maxVel;
    public float accSpeed;
    public float decSpeed;
    bool stopMovingX;
    bool stopMovingZ;
    Vector3 newPos;
    public float skinWidth;
    bool isOverlapping;
    public LayerMask mask;
    public BoxCollider playerCollider;
    public Vector3 MTV;
    bool hasJumped;
    public float jumpHeight;

    void Start()
    {
        stopMovingX = false;
        stopMovingZ = false;
        hasJumped = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Vector3(playerCollider.transform.position.x, playerCollider.transform.position.y - 0.5f, playerCollider.transform.position.z), Vector3.down);
    }

    private void FixedUpdate()
    {
        Physics.Raycast(new Vector3(playerCollider.transform.position.x,playerCollider.transform.position.y - 0.5f, playerCollider.transform.position.z), Vector3.down, out RaycastHit hit);

        // Debug.Log(hit.distance); // TODO come back to this!

        if (Input.GetKey(KeyCode.D))
        {
            stopMovingX = false;
            velocity.x += accSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            stopMovingX = false;
            velocity.x -= accSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            stopMovingZ = false;
            velocity.z += accSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            stopMovingZ = false;
            velocity.z -= accSpeed;
        }

        if (velocity.x > maxVel)
        {
            velocity.x = maxVel;
        }
        if (velocity.x < 0 - maxVel)
        {
            velocity.x = 0 - maxVel;
        }
        if (velocity.z > maxVel)
        {
            velocity.z = maxVel;
        }
        if (velocity.z < 0 - maxVel)
        {
            velocity.z = 0 - maxVel;
        }

        if (Input.GetKey(KeyCode.Space) && !hasJumped)
        {
            velocity.y += jumpHeight;
            hasJumped = true;
        }

        velocity += Physics.gravity * Time.deltaTime;

        Vector3 sizeOrig = playerCollider.size;
        Vector3 sizeWithSkin = playerCollider.size + Vector3.one * skinWidth;
        playerCollider.size = sizeWithSkin;

        newPos = rbody.position + velocity * Time.fixedDeltaTime;

        Collider[] colliders = Physics.OverlapBox(newPos, new Vector3(0.5f + skinWidth/2, 0.5f + skinWidth/2, 0.5f + skinWidth/2), rbody.rotation, mask);

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].name != playerCollider.name)
            {
                isOverlapping = Physics.ComputePenetration(playerCollider, newPos, rbody.rotation, colliders[i], colliders[i].transform.position, colliders[i].transform.rotation, out Vector3 direction, out float distance);
                MTV = direction * distance;
                newPos += MTV;

                velocity = Vector3.ProjectOnPlane(velocity, MTV);
            }
        }

        playerCollider.size = sizeOrig;
        rbody.MovePosition(rbody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D))
        {
            stopMovingX = true;
        }
        if(!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S))
        {
            stopMovingZ = true;
        }
        
        if (stopMovingX)
        {
            // For an immediate stop
            //velocity.x = 0.0f;
            //stopMovingX = false;
            // For a gradual stop
            if (velocity.x > 0)
            {
                velocity.x -= decSpeed * Time.deltaTime;
                if (velocity.x <= 0)
                {
                    velocity.x = 0;
                    stopMovingX = false;
                }
            }
            else
            {
                velocity.x += decSpeed * Time.deltaTime;
                if (velocity.x >= 0)
                {
                    velocity.x = 0;
                    stopMovingX = false;
                }
            }
        }
        if (stopMovingZ)
        {
            // For an immediate stop
            //velocity.z = 0.0f;
            //stopMovingZ = false;
            // For a gradual stop
            if (velocity.z > 0)
            {
                velocity.z -= decSpeed * Time.deltaTime;
                if (velocity.z <= 0)
                {
                    velocity.z = 0;
                    stopMovingZ = false;
                }
            }
            else
            {
                velocity.z += decSpeed * Time.deltaTime;
                if (velocity.z >= 0)
                {
                    velocity.z = 0;
                    stopMovingZ = false;
                }
            }
        }
    }
}