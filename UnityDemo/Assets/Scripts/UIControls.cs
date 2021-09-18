﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    public new Camera camera;

    public Rigidbody swingNormal;
    public Rigidbody swingTire;
    public Ragdoll bullyRag;
    public Rigidbody bullyRB;
    public Collider bullyColl;
    public Rigidbody bullyHips;

    float swingVelCheck;
    float swingHeightCheck;
    bool swingTooHigh;
    
    float tireVelCheck;
    float tireHeightCheck;
    public bool tireTooHigh;

    public bool testBool;
    public bool punish;

    public TextMesh text;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, camera.ScreenPointToRay(Input.mousePosition).direction * 10000);
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;

        testBool = Physics.Raycast(transform.position, camera.ScreenPointToRay(Input.mousePosition).direction, out RaycastHit hit, 10000);

        switch (hit.collider.tag)
        {
            case "Swing":
                text.text = "Click to push swing";
                break;
            case "TireSwing":
                text.text = "Click to push tire swing";
                break;
            case "Brat":
                if (bullyRag.hasBeenRude)
                {
                    text.text = "Click to punish this child";
                }
                else
                {
                    text.text = "Click to push swing";
                }
                break;
            case "Head":
                text.text = "Click to move this child";
                break;
            default:
                text.text = "";
                break;
        }

        swingVelCheck = Mathf.Abs(swingNormal.velocity.z);

        if (swingVelCheck <= 0.05)
        {
            swingHeightCheck = swingNormal.position.y;
        }

        if (swingHeightCheck >= 5)
        {
            swingTooHigh = true;
        }
        else
        {
            swingTooHigh = false;
        }

        tireVelCheck = Mathf.Abs(swingTire.velocity.z);

        if (tireVelCheck <= 0.05)
        {
            tireHeightCheck = swingTire.position.y;
        }

        if(tireHeightCheck >= 5)
        {
            tireTooHigh = true;
        }
        else
        {
            tireTooHigh = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (hit.collider.tag)
            {
                case "Swing":
                    if (swingTooHigh)
                    {
                        Debug.Log("Too high!");
                    }
                    else
                    {
                        swingNormal.AddForce(0.0f, 0.0f, -0.5f, ForceMode.Impulse);
                    }
                    break;
                case "TireSwing":
                    if (tireTooHigh)
                    {
                        Debug.Log("Too high!");
                    }
                    else
                    {
                        swingTire.AddForce(Random.Range(-0.5f, 0.5f), 0.0f, -0.5f, ForceMode.Impulse);
                    }
                    break;
                case "Brat":
                    if (bullyRag.hasBeenRude)
                    {
                        bullyRag.isRagdolling = true;
                        bullyRag.isBitching = false;
                        bullyRag.drama.SetActive(true);
                        // bullyRB.AddForce(30.0f, 30.0f, 30.0f, ForceMode.Impulse);
                        bullyColl.enabled = false;
                        punish = true;
                    }
                    else
                    {
                        if (swingTooHigh)
                        {
                            Debug.Log("Too high!");
                        }
                        else
                        {
                            swingNormal.AddForce(0.0f, 0.0f, -0.5f, ForceMode.Impulse);
                        }
                    }
                    break;
                default:
                    text.text = "";
                    break;
            }
        }
        if(Input.GetKeyUp(KeyCode.Mouse0) && punish)
        {
            punish = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.CompareTag("Head") && !punish)
        {
            bullyRB.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 10.0f, Random.Range(-20.0f, 10.0f)); // x is side to side, z is closer or farther from camera
            bullyHips.rotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        }
    }
}