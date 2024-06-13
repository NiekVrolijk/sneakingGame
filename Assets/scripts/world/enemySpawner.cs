using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemySpawner : MonoBehaviour
{
    public static int enemysKilled = 0;

    void Update()
    {
        if (enemysKilled >= 10)
        {
            SceneManager.LoadScene("winscreen");
            enemysKilled = 0;
        }
    }
}
