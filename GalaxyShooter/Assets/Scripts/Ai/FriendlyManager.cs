using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyManager : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;

   // public LayerMask whatIsGround;
   // public LayerMask Default;

    public float health;
    public float minimumDistance;

    public int damage;
    GameObject[] enemies;
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

    // states 
    //public float sightRange;
    //public float attackRange;

    //public bool enemyInSightRange;
    //public bool enemyInAttackRange;

    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isAttacking = false;
        isPatroling = true;
        animator.SetBool("isPatroling", true);
    }

    private void Update()
    {
        // checks for the sight and attack range.
        //enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, Default);
        //enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, Default);

        for(int i = 0; i < enemies.Length; i++)
        {
            if(Vector3.Distance(player.transform.position, enemies[i].transform.position) <= minimumDistance)
            {
                Debug.Log(enemies[i]);
                enemy = enemies[i];
                Attacking(enemy);
            }
        }

        //if (enemyInSightRange && enemyInAttackRange) Attacking();
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
