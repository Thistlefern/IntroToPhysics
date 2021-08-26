using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0.0f, 20.0f, -6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
