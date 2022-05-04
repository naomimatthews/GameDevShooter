using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyManager : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;



    public float health;
    public float minimumDistance;

    public int damage;
   // GameObject[] enemies;
   public List<GameObject> enemies;
    private GameObject enemy;
    private GameObject enemyObj;

    // animations
    [SerializeField] Animator animator;
    bool isAttacking;
    bool isPatroling;

    // attacking
    [SerializeField] Transform attackPoint;
    [SerializeField] public GameObject projectile;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    private void Awake()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        enemyObj = new GameObject();
    }

    private void Start()
    {
        isAttacking = false;
        isPatroling = true;
        animator.SetBool("isPatroling", true);
    }

    private void Update()
    {

        for(int i = 0; i < enemies.Count; i++)
        {
            if(Vector3.Distance(player.transform.position, enemies[i].transform.position) <= minimumDistance)
            {
                enemy = enemies[i];
                Attacking(enemy);
            }
        }
    }

    private void Attacking(GameObject enemy)
    {
        // stop enemy movement.
        navMeshAgent.SetDestination(transform.position);

        enemyObj.transform.position = enemy.transform.position;

        transform.LookAt(enemyObj.transform);

        if (!alreadyAttacked)
        {
            isAttacking = true;

            // attack player.
            animator.SetBool("isAttacking", true);
            animator.SetBool("isPatroling", false);

            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("isAttacking", false);
    }


    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
