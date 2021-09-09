using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody seat;
    public Rigidbody butt;
    public GameObject drama;
    Vector3 tempRotation;
    Vector3 tempPos;
    public bool isRagdolling;
    public bool isBitching;
    float velocityCheck;
    float heightCheck;

    private void Start()
    {
        isRagdolling = false;
        tempRotation = Vector3.zero;
        drama.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isRagdolling)
        {
            tempRotation.x = 115 * seat.transform.rotation.x;
            tempRotation.x -= 90;
            butt.rotation = Quaternion.Euler(tempRotation);

            tempPos = seat.transform.position;
            tempPos.y += 0.57f;
            butt.position = tempPos;
        }
        else
        {
            drama.SetActive(true);
        }
    }

    private void Update()
    {
        velocityCheck = Mathf.Abs(seat.velocity.z);

        if(velocityCheck <= 0.05)
        {
            heightCheck = seat.position.y;
        }

        if(heightCheck < 1.3 && !isRagdolling)
        {
            isBitching = true; // TODO the kid should bitch at the player
        }
        else
        {
            isBitching = false;
        }
    }
}
