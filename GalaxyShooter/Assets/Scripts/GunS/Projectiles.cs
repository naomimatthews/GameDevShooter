using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    // stores damage the projectile will do
    public int damage = 30;

    // stores the explosion the projectile will make when it hits something
    public GameObject Explosion;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth damagePlayer = collision.gameObject.GetComponent<PlayerHealth>();

            damagePlayer.TakeDamage(damage);

            Debug.Log("enemy hit player");

            //make the explosion
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);

            //destroy the projectile
            Destroy(gameObject);

        }

        if (collision.gameObject.GetComponent<AllyHealth>() != null)
        {
            AllyHealth damageAlly = collision.gameObject.GetComponent<AllyHealth>();

            damageAlly.TakeDamage(damage);

            Debug.Log("enemy hit ally");

            //make the explosion
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);

            //destroy the projectile
            Destroy(gameObject);

        }

        if (collision.gameObject.GetComponent<Damageable>() != null)
        {
            Damageable damageEnemy = collision.gameObject.GetComponent<Damageable>();

            damageEnemy.TakeDamage(damage);

            Debug.Log("ally hit enemy");

            //make the explosion
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);

            //destroy the projectile
            Destroy(gameObject);

        }
    }
}
