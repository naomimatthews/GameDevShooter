using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControls : MonoBehaviour
{
    NavMeshAgent agent;

    private Rigidbody rb;

    public GameObject waypoints;

    int waypointIndex;
    Vector3 target;

    float xWanderRange;
    float zWanderRange;


    float currentTime;
    public float startTime;

    private float freezeDur = 1.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypointIndex = 0;
        UpdateDestination();
    }

    void Update()
    {
        // checking if ai has reached destination or last waypoint.
        if (waypointIndex == waypoints.transform.childCount - 1 && agent.remainingDistance < 0.5f)
        {
            xWanderRange = Random.Range(-10.0f, 10.0f);
            zWanderRange = Random.Range(-10.0f, 10.0f);

            // checks that the distance is big enough to move enemy.
            if ((xWanderRange < -5.0f || xWanderRange > 5.0f) && (zWanderRange < -5.0f || zWanderRange > 5.0f))
            {
                Vector3 targetPosition = new Vector3(xWanderRange, 0, zWanderRange) + transform.position;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(targetPosition, out hit, 1.0f, NavMesh.AllAreas))
                {
                    agent.destination = hit.position;
                }
            }
        }

        else if (agent.remainingDistance < 1.0f)
        {
           // Debug.Log("Reached destination");
            IterateWaypointIndex();
            UpdateDestination();
        }
    }
    void UpdateDestination()
    {
        target = waypoints.transform.GetChild(waypointIndex).position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.transform.childCount)
        {
            waypointIndex = 0;
        }
    }

    public void StopEnemy()
    {
        currentTime = startTime;

        // make enemy destination their position.
        agent.destination = transform.position;
       // rb.constraints = RigidbodyConstraints.FreezeAll;

    }
}


