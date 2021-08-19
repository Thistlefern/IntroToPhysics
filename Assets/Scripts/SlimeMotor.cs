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
        distanceToFriendX = rbody.position.x - friend.position.x;
        distanceToFriendZ = rbody.position.z - friend.position.z;
        distanceToFriend = Mathf.Abs(distanceToFriendX) + Mathf.Abs(distanceToFriendZ);

        // Debug.Log(distanceToFriend);
        // Debug.Log("X: " + Mathf.Abs(distanceToFriendX));
        // Debug.Log("Z: " + Mathf.Abs(distanceToFriendZ));

        // transform.LookAt(friend.position);

        transform.rotation = Quaternion.Euler(0.0f, (distanceToFriendX + distanceToFriendZ) * 10, 0.0f).normalized;

        if (distanceToFriend <= reachedThreshold)
        {
            closeEnough = true;
        }
        else
        {
            closeEnough = false;
        }

        //if (!closeEnough)
        //{
        //    if (jumpAgain)
        //    {
        //        rbody.AddForce(transform.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);

        //        jumpAgain = false;
        //    }
        //}

        //if (!jumpAgain)
        //{
        //    jumpTimer += Time.deltaTime;
        //    if(jumpTimer >= jumpFrequency)
        //    {
        //        jumpTimer = 0.0f;
        //        jumpAgain = true;
        //    }
        //}
    }
}
