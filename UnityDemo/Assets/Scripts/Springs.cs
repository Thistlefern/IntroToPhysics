using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springs : MonoBehaviour
{
    public Rigidbody rbody;
    public Rigidbody connectTo;
    public float tightness;
    float force;
    Vector3 direction;
    Vector3 offset = new Vector3(0.0f, -4.0f, 0.0f);
    Vector3 moveTo;
    float damper;
    public float dampingCoeff;
    Vector3 relVel;

    public Vector3 restingPos;

    public void FixedUpdate()
    {
        restingPos = connectTo.position - offset;

        relVel = connectTo.velocity - rbody.velocity;
        damper = (dampingCoeff * relVel).magnitude;

        force = (-1 * tightness * (restingPos - rbody.position).magnitude) - damper;

        Debug.DrawLine(connectTo.position, rbody.position, Color.red);

        rbody.AddForce((rbody.position - restingPos) * force * tightness);
    }
}