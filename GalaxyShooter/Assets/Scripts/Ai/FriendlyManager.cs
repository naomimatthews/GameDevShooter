using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyManager : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    private Transform enemy;
    private GameObject enemyObj;

    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;

    public float health;

    public int damage;

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
    public float sightRange;
    public float attackRange;

    public bool enemyInSightRange;
    public bool enemyInAttackRange;

    private void Awake()
    {
       enemyObj = GameObject.FindGameObjectWithTag("Enemy");
        enemy = enemyObj.transform;

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
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        if (enemyInSightRange && enemyInAttackRange) Attacking();
    }

    private void Attacking()
    {
        // stop enemy movement.
        navMeshAgent.SetDestination(transform.position);

        transform.LookAt(enemy);

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
