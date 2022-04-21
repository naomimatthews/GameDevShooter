using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knives : MonoBehaviour
{
    public int damage;

    private Rigidbody rb;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Damageable>() != null)
        {
            Damageable damageEnemy = collision.gameObject.GetComponent<Damageable>();

            damageEnemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
