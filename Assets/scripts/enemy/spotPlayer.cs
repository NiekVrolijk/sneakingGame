using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotPlayer : MonoBehaviour
{
    public static bool playerSpotted = false;

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 rayOrigin = transform.position;

        Vector3 rayDirection = transform.forward;

        ray = new Ray(rayOrigin, rayDirection);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("player"))
            {
                Debug.Log("player seen");
                playerSpotted = true;
            }
            else
            {
                playerSpotted = false;
            }
        }
    }
}
