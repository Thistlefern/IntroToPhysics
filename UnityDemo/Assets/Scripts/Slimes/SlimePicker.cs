using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePicker : MonoBehaviour
{
    public new Camera camera;

    public bool slimeSelected;

    void Start()
    {
        slimeSelected = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(camera.ScreenPointToRay(Input.mousePosition));
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        Physics.Raycast(ray,out RaycastHit hit);

        if (hit.collider.CompareTag("Slime"))
        {
            slimeSelected = true;
        }
        else
        {
            slimeSelected = false;
        }
    }
}
