using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;

    [SerializeField] GameObject hitMarker;
    [SerializeField] GameObject enemies;

    Rigidbody rb;

    void Start()
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


    void Die()
    {
       // Debug.Log("was destroyed");
        Destroy(gameObject);
    }
}
