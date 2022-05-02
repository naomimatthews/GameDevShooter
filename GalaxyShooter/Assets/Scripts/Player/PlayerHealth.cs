using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    float maxHealth = 900f;
    float currentHealth;

    public TextMeshProUGUI healthText;
    public GameObject deathCamera;

    public static bool playerAlive = true;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthText.SetText( currentHealth + "   ");
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
        playerAlive = false;
        Destroy(gameObject);
        deathCamera.SetActive(true);
    }
}
