using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("you lost");
            SceneManager.LoadScene("deathscreen");
            Destroy(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
