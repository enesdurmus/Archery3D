using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour, IMovable
{
    [SerializeField] private GameObject target;
    private Rigidbody physic;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        physic = GetComponent<Rigidbody>();
    }

    void Update()
    {
        handleMovement();
    }


    public void handleMovement()
    {
        agent.destination = target.transform.position; 
    }

    public void SetEnemySpeed(float speed){

        agent.speed = speed;
    }
}
