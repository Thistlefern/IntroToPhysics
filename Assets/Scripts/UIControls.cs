using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    public Camera camera;

    public Rigidbody swingNormal;
    public Rigidbody swingTire;

    float swingVelCheck;
    float swingHeightCheck;
    bool swingTooHigh;
    
    float tireVelCheck;
    float tireHeightCheck;
    public bool tireTooHigh;

    public bool testBool;

    public TextMesh text;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, camera.ScreenPointToRay(Input.mousePosition).direction * 20);
    }

    void Update()
    {
        testBool = Physics.Raycast(transform.position, camera.ScreenPointToRay(Input.mousePosition).direction, out RaycastHit hit, 20);

        switch (hit.collider.tag)
        {
            case "Swing":
                text.text = "Swing!";
                break;
            case "TireSwing":
                text.text = "Tire Swing!";
                break;
            case "Brat":
                text.text = "A Doucebag!";
                break;
            default:
                text.text = "";
                break;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(tireTooHigh)
            {
                Debug.Log("Too high!");
            }
            else
            {
                swingTire.AddForce(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, -1.0f), ForceMode.Impulse);
            }
        }
    }
}
