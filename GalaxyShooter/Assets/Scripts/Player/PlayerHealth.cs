using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    float maxHealth = 100f;
    float currentHealth;

    public TextMeshProUGUI healthText;
    public GameObject deathCamera;

    public static bool playerAlive = true;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
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

    public void Die()
    {
        playerAlive = false;
        Destroy(gameObject);
       // gameObject.SetActive(false);
        deathCamera.SetActive(true);
    }
}
