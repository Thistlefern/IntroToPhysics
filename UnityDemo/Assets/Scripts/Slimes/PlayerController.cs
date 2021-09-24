using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public Rigidbody rbody;
    public float jumpHeight;
    public Ragdoll bully;
    public Animator animator;

    public bool isRunning;
    public bool hasJumped;
    public bool isFalling;

    public float happiness;
    public TMPro.TMP_Text happinessText;
    public bool onSwing;
    public bool onTireSwing;

    Vector3 rotN = new Vector3(0.0f, 180.0f, 0.0f);
    Vector3 rotNE = new Vector3(0.0f, 225.0f, 0.0f);
    Vector3 rotE = new Vector3(0.0f, 270.0f, 0.0f);
    Vector3 rotSE = new Vector3(0.0f, 315.0f, 0.0f);
    Vector3 rotS = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 rotSW = new Vector3(0.0f, 45.0f, 0.0f);
    Vector3 rotW = new Vector3(0.0f, 90.0f, 0.0f);
    Vector3 rotNW = new Vector3(0.0f, 135.0f, 0.0f);

    // ------ When you forget that OnCollisionEnter exists
    //bool raycast1;
    //bool raycast2;
    //bool raycast3;
    //bool raycast4;

    //Vector3 raycast1Pos = new Vector3(0.0f, 0.75f, 6.5f);
    //Vector3 raycast2Pos = new Vector3(-0.5f, 0.75f, 7.0f);
    //Vector3 raycast3Pos = new Vector3(0.0f, 0.75f, 7.5f);
    //Vector3 raycast4Pos = new Vector3(0.5f, 0.75f, 7.0f);
    // ------ End of silliness ------

    void Start()
    {
        hasJumped = false;
        isFalling = false;
        happiness = 40;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Swing"))
        {
            if (hasJumped)
            {
                onSwing = true;
            }
            else
            {
                onSwing = false;
            }
        }
        else
        {
            onSwing = false;
        }
        if(collision.collider.CompareTag("TireSwing"))
        {
            if (hasJumped)
            {
                onTireSwing = true;
            }
            else
            {
                onTireSwing = false;
            }
        }
        else
        {
            onTireSwing = false;
        }
        if(collision.collider.name == "Plane")
        {
            hasJumped = false;
            animator.SetBool("HasJumped", false);
            isFalling = false;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
            isRunning = true;
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(rotNW);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(rotNE);
            }
            else
            {
                transform.rotation = Quaternion.Euler(rotN);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
            isRunning = true;
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(rotSW);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(rotSE);
            }
            else
            {
                transform.rotation = Quaternion.Euler(rotS);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            // transform.position -= transform.right * speed * Time.deltaTime;
            isRunning = true;
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                transform.position += transform.forward * speed * Time.fixedDeltaTime;
                transform.rotation = Quaternion.Euler(rotW);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            // transform.position += transform.right * speed * Time.deltaTime;
            isRunning = true;
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                transform.position += transform.forward * speed * Time.fixedDeltaTime;
                transform.rotation = Quaternion.Euler(rotE);
            }
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.Space) && !hasJumped)
        {
            isRunning = false;
            rbody.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            hasJumped = true;
            animator.SetBool("HasJumped", true);
        }

        if (isRunning)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if(rbody.velocity.y < 0)
        {
            isFalling = true;
        }



        //if(rbody.velocity.y == 0 && isFalling)
        //{
        //    hasJumped = false;
        //    animator.SetBool("HasJumped", false);
        //    isFalling = false;
        //}
    }

    private void Update()
    {
        if (onTireSwing)
        {
            happiness += Time.deltaTime;
        }
        if (onSwing)
        {
            happiness += Time.deltaTime * 1.5f;
        }

        if(bully.isBitching)
        {
            happiness -= Time.deltaTime * 0.5f;
        }

        if (bully.hasBeenRude && !bully.isRagdolling)
        {
            happiness -= Time.deltaTime * 1.25f;
        }

        if (bully.damperOn)
        {
            happinessText.text = "Happiness: " + Mathf.CeilToInt(happiness).ToString();
        }
        else
        {
            happinessText.text = "";
        }

        // ------ Infinite loop, oops
        //while (onSwing)
        //{
        //    happiness += Time.deltaTime;
        //}

        // ------ When you forget that OnCollisionEnter exists
        //raycast1Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
        //raycast1 = Physics.Raycast(raycast1Pos, transform.up * -1, out RaycastHit hit1);
        //raycast2Pos = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        //raycast2 = Physics.Raycast(raycast2Pos, transform.up * -1, out RaycastHit hit2);
        //raycast3Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
        //raycast3 = Physics.Raycast(raycast3Pos, transform.up * -1, out RaycastHit hit3);
        //raycast4Pos = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        //raycast4 = Physics.Raycast(raycast4Pos, transform.up * -1, out RaycastHit hit4);

        //if(hit1.collider.name == "Swing" || hit1.collider.name == "Tire Swing")
        //{
        //    Debug.Log("Hooray!");
        //}
        // ------ End of silliness ------
    }
}