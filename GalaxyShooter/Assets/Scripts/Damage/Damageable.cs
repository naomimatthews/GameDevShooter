using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;

    public static int numOfEnemies = 5;

    Rigidbody rb;

    // get list.

    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
        numOfEnemies--;
        FriendlyManager.singleton.DestroyEnemy(gameObject);
    }
}
