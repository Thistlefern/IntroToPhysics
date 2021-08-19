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
    public bool friendSpotted;
    public bool closeEnough;
    public float sightDistance;

    public Vector3 lookDirection;
    public float rotationSpeed;

    public float jumpFrequency;
    public float jumpTimer;
    bool jumpAgain;
    public int jumpHeight;
    public int jumpDistance;

    public float randTimer;
    Vector3 randLocation;

    void Start()
    {
        friendSpotted = false;
        closeEnough = false;
        jumpTimer = 0.0f;
        jumpAgain = false;

        jumpFrequency = Random.Range(0.5f, 1.5f);

        randTimer = 0.0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * sightDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, randLocation - transform.position);
    }

    void Update()
    {
        friendSpotted = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, sightDistance);
                
        distanceToFriendX = rbody.position.x - friend.position.x;
        distanceToFriendZ = rbody.position.z - friend.position.z;
        distanceToFriend = Mathf.Abs(distanceToFriendX) + Mathf.Abs(distanceToFriendZ);

        if (distanceToFriend <= reachedThreshold) // TODO maybe adjust for line of sight
        {
            closeEnough = true;
        }
        else
        {
            closeEnough = false;
        }

        if (friendSpotted)
        {
            randTimer = 0.0f;
            randLocation = friend.position;

            lookDirection = friend.position - transform.position;
            lookDirection.y = 0.0f;
            transform.rotation = Quaternion.LookRotation(lookDirection);

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
        else
        {
            randTimer += Time.deltaTime;

            if(randTimer >= 5)
            {
                Debug.Log("New location");
                float randX = Random.Range(0.0f, 100.0f);
                float randZ = Random.Range(0.0f, 100.0f);
                randLocation = new Vector3(randX, 0.0f, randZ);
                randTimer = 0.0f;
            }

            lookDirection = randLocation- transform.position;
            Quaternion randLookDirection = Quaternion.LookRotation(randLocation - transform.position);
            lookDirection.y = 0.0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, randLookDirection, Time.deltaTime * rotationSpeed);
        }
    }
}
