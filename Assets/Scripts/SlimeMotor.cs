using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMotor : MonoBehaviour
{
    public Rigidbody rbody;
    public Rigidbody friend;
    float distanceToFriendX;
    float distanceToFriendZ;
    float distanceToFriend;
    public float reachedThreshold;
    public bool closeEnough;

    public Vector3 lookDirection;
    public float rotationSpeed;

    public int jumpFrequency;
    public float jumpTimer;
    bool jumpAgain;
    public int jumpHeight;
    public int jumpDistance;

    void Start()
    {
        closeEnough = false;
        jumpTimer = 0.0f;
        jumpAgain = false;
    }

    void Update()
    {
        lookDirection = friend.position - transform.position;
        lookDirection.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(lookDirection);
                
        distanceToFriendX = rbody.position.x - friend.position.x;
        distanceToFriendZ = rbody.position.z - friend.position.z;
        distanceToFriend = Mathf.Abs(distanceToFriendX) + Mathf.Abs(distanceToFriendZ);

        if (distanceToFriend <= reachedThreshold)
        {
            closeEnough = true;
        }
        else
        {
            closeEnough = false;
        }

        if (!closeEnough)
        {
            if (jumpAgain)
            {
                rbody.AddForce(transform.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
                rbody.AddForce(transform.forward * jumpDistance * Time.deltaTime, ForceMode.Impulse);
                jumpAgain = false;
            }
        }

        if (!jumpAgain)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpFrequency)
            {
                jumpTimer = 0.0f;
                jumpAgain = true;
            }
        }
    }
}
