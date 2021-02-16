using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour
{
    private GameObject target;
    NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }


    public void handleMovement()
    {
        if (target != null)
            agent.destination = target.transform.position;
    }

    public void SetEnemySpeed(float speed)
    {
        agent.speed = speed;
    }
}
