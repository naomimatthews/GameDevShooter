using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform player;
    [SerializeField] GameObject playerObject;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

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

    public bool playerInSightRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
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
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

       // if (playerInSightRange && !playerInAttackRange) Chasing();
       // if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Chasing()
    {
        animator.SetBool("isPatroling", true);
        navMeshAgent.SetDestination(player.position);
    }

    private void Attacking()
    {
        // stop enemy movement.
        navMeshAgent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            isAttacking = true;

            // attack player.
            animator.SetBool("isAttacking", true);
            animator.SetBool("isPatroling", false);

            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 32f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth damagePlayer = collision.gameObject.GetComponent<PlayerHealth>();

            damagePlayer.TakeDamage(damage);

            Debug.Log("enemy hit player");

            Destroy(gameObject);
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
