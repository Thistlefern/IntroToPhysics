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
    float randRot;

    public int happiness;
    // public bool megaSlime;
    public GameObject megaArt;

    void Start()
    {
        friendSpotted = false;
        closeEnough = false;
        jumpTimer = 0.0f;
        jumpAgain = false;

        reachedThreshold = 3.5f;
        sightDistance = 10;
        rotationSpeed = 20;
        jumpDistance = 150;

        jumpFrequency = Random.Range(1.0f, 2.0f);

        randTimer = 0.0f;

        happiness = 0;
        //megaSlime = false;
        megaArt.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + new Vector3(0.0f, 1.0f, 0.0f), transform.forward * sightDistance);
    }

    void Update()
    {
        friendSpotted = Physics.Raycast(transform.position + new Vector3(0.0f, 1.0f, 0.0f), transform.forward, out RaycastHit hit, sightDistance);
                
        distanceToFriendX = rbody.position.x - friend.position.x;
        distanceToFriendZ = rbody.position.z - friend.position.z;
        distanceToFriend = Mathf.Abs(distanceToFriendX) + Mathf.Abs(distanceToFriendZ);

        if (happiness >= 10)
        {
            // megaSlime = true;
            reachedThreshold = 16;
            sightDistance = 40;
            rotationSpeed = 15;
            jumpDistance = 200;
            megaArt.SetActive(true);
        }

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
            randRot = (friend.position - transform.position).normalized.y;

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
                randRot = Random.Range(-180.0f, 180.0f);
                randTimer = 0.0f;
                Whatever();
            }
        }
    }
    [ContextMenu("Whatever")]
    public void Whatever()
    {
        Quaternion randLookDirection = Quaternion.Euler(new Vector3(0.0f, randRot, 0.0f));
        transform.rotation = randLookDirection;
    }
}