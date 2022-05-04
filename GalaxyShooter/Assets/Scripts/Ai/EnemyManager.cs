using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform enemy;
    public List<GameObject> players;

    public float health;

    public int damage;

    //GameObject[] players;
    private GameObject closePlayer;
    private GameObject playerObj;

    public float minimumDistance;

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

    public void Awake()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        playerObj = new GameObject();
    }

    public void Start()
    {
        isAttacking = false;
        isPatroling = true;
        animator.SetBool("isPatroling", true);
    }

    private void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (Vector3.Distance(enemy.transform.position, players[i].transform.position) <= minimumDistance)
            {
                closePlayer = players[i];
                Attacking(closePlayer);
            }
        }
    }

    private void Attacking(GameObject closePlayer)
    {
        // stop enemy movement.
        navMeshAgent.SetDestination(transform.position);

        playerObj.transform.position = closePlayer.transform.position;

        transform.LookAt(playerObj.transform);

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
