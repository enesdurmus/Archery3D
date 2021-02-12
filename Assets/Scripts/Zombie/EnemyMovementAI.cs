using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour
{
    [SerializeField] private GameObject target;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    public void handleMovement()
    {
        agent.destination = target.transform.position; 
    }

    public void SetEnemySpeed(float speed){

        agent.speed = speed;
    }
}
