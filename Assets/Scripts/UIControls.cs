using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    public Camera camera;

    public Rigidbody swingNormal;
    public Rigidbody swingTire;
    public Ragdoll bratRag;
    public Rigidbody bratRB;
    public Collider bratColl;
    public Rigidbody bratHips;

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
                text.text = "Click to punish this child";
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
                        swingNormal.AddForce(0.0f, 0.0f, -1.0f, ForceMode.Impulse);
                    }
                    break;
                case "TireSwing":
                    if (tireTooHigh)
                    {
                        Debug.Log("Too high!");
                    }
                    else
                    {
                        swingTire.AddForce(Random.Range(-0.5f, 0.5f), 0.0f, -1.0f, ForceMode.Impulse);
                    }
                    break;
                case "Brat":
                    bratRag.isRagdolling = true;
                    bratRag.isBitching = false;
                    bratRag.drama.SetActive(true);
                    bratRB.AddForce(30.0f, 30.0f, 30.0f, ForceMode.Impulse);
                    bratColl.enabled = false;
                    punish = true;
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.tag == "Head" && !punish)
        {
            Debug.Log("Time Out");
            bratRB.transform.position = new Vector3(Random.Range(-15.0f, 15.0f), 10.0f, Random.Range(-30.0f, 20.0f)); // x is side to side, z is closer or farther from camera
            bratHips.rotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        }
    }
}
