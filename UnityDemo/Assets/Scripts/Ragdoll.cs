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
    public bool isBitching;
    public bool isRagdolling;
    public TextMesh bitchText;
    float velocityCheck;
    float heightCheck;
    float rudeTimer;
    float swingTimer;
    public bool hasBeenRude;

    private void Start()
    {
        tempRotation = Vector3.zero;
        drama.SetActive(false);
        isRagdolling = false;
        rudeTimer = 0;
        hasBeenRude = false;
    }

    private void FixedUpdate()
    {
        tempRotation.x = 115 * seat.transform.rotation.x;
        tempRotation.x -= 90;
        butt.rotation = Quaternion.Euler(tempRotation);

        tempPos = seat.transform.position;
        tempPos.y += 0.57f;
        butt.position = tempPos;
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
            isBitching = true;
        }
        else
        {
            isBitching = false;
        }

        if (!isRagdolling)
        {
            if (isBitching)
            {
                swingTimer = 0;
                rudeTimer += Time.deltaTime;
                if (rudeTimer >= 5 && rudeTimer < 10)
                {
                    bitchText.text = "I said push me!!";
                }
                else if(rudeTimer >= 10 && rudeTimer < 20)
                {
                    bitchText.text = "Are you deaf or just stupid?!";
                }
                else if(rudeTimer >= 20)
                {
                    bitchText.text = "You're as stupid as your kid!";
                    hasBeenRude = true;
                }
                else
                {
                    bitchText.text = "Give me a push!";
                }
            }
            else
            {
                rudeTimer = 0;
                swingTimer += Time.deltaTime;
                if(swingTimer < 5)
                {
                    bitchText.text = "Wheee!";
                }
                else if(swingTimer >= 5 && swingTimer <= 15)
                {
                    bitchText.text = "";
                }
                else
                {
                    bitchText.text = "See kid? This is how\nyou stay on a swing!";
                    hasBeenRude = true;
                }
            }
        }
        else
        {
            bitchText.text = "";
        }
    }
}
