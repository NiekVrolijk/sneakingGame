using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackDamage : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemySpawner.enemysKilled++;
            Destroy(other.gameObject);
        }
    }
}