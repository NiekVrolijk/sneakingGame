using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("you lost");
            Destroy(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
