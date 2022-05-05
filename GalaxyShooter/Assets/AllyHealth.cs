using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyHealth : MonoBehaviour
{
    float maxHealth = 100f;
    float currentHealth;

    private Rigidbody rb;

    public static int numOfAllies = 4;

    public void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        numOfAllies--;
        EnemyManager.singleton.DestroyPlayer(gameObject);
        Debug.Log("ally dead");
        Destroy(gameObject);
    }
}
