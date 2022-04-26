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

    public void Freeze()
    {
        Debug.Log("FREEZE");

        // freeze enemy.
        // disable the enemy controls script.
        enemies.GetComponentInChildren<EnemyControls>().enabled = false;

        rb.constraints = RigidbodyConstraints.FreezePosition;

    }

    public void UnFreeze()
    {
        // unfreeze enemy.
        rb.constraints = RigidbodyConstraints.None;
    }

    void Die()
    {
       // Debug.Log("was destroyed");
        Destroy(gameObject);
    }
}
