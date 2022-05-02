using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyHealth : MonoBehaviour
{
    float maxHealth = 100f;
    float currentHealth;

    public static int numOfAllies = 4;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        numOfAllies--;
        Destroy(gameObject);
    }
}
