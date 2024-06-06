using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    float attackCooldown = 0.5f;
    float attackDuration = 0.2f;
    float cooldownTimer;

    public GameObject attackSwing;

    private void Start()
    {
        attackSwing.SetActive(false);
    }

    void Update()
    {
        Attack();
    }
    void Attack()
    {
        cooldownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer >= attackCooldown)
        {
            attackSwing.SetActive(true);
            cooldownTimer = 0;
        }

        if (cooldownTimer > attackDuration)
        {
            attackSwing.SetActive(false);
        }
    }
}
