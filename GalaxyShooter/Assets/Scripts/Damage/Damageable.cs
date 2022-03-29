using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;

    [SerializeField] GameObject hitMarker;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        //Instantiate(hitMarker, hitPos, Quaternion.LookRotation(hitNormal));
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("was destroyed");
        Destroy(gameObject);
    }
}
