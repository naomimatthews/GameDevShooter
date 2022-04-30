using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    // stores damage the projectile will do
    public int damage;

    // stores the explosion the projectile will make when it hits something
    public GameObject Explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth damagePlayer = collision.gameObject.GetComponent<PlayerHealth>();

            damagePlayer.TakeDamage(damage);

            Debug.Log("enemy hit player");

            //make the explosion
            GameObject ThisExplosion = Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

            //destory the projectile
            Destroy(gameObject);

        }

        if (collision.gameObject.GetComponent<AllyHealth>() != null)
        {
            AllyHealth damageAlly = collision.gameObject.GetComponent<AllyHealth>();

            damageAlly.TakeDamage(damage);

            Debug.Log("enemy hit player");

            //make the explosion
            GameObject ThisExplosion = Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

            //destory the projectile
            Destroy(gameObject);

        }
    }
}
