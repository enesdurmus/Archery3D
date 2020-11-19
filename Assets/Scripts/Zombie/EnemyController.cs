using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnimator;
    private bool isEnemyRoared = false;
    [SerializeField] private GameObject player;

    public float attackPower {get; set;}
    public float Healt {get; set;}

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        Healt = 20f;
        attackPower = 10f;
    }

    void Update()
    {
        GetComponent<EnemyMovementAI>().handleMovement();

        if (Healt > 0)
        {
            AttackControl();
        } 
    }   

    public void Attack()
    {
        enemyAnimator.SetBool("isEnemyAttacked", true);
    }

    public void AttackControl()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 1.5f)
        {
            Attack();
        }
        else if (Vector3.Distance(player.transform.position, transform.position) <= 7f)
        {
            if (!isEnemyRoared)
            {
                enemyAnimator.SetBool("isEnemyRoaring", true);
            } 
        }
    }
   
    public void TakeDamage(float damage)
    {
        Healt -= damage;
        EnemyDie();

        if(Healt != 0) {
            enemyAnimator.SetBool("enemyReact", true);
        }
        
        Debug.Log(Healt);
    }

    public void EnemyDie()
    {
        if(Healt == 0)
        {
            enemyAnimator.SetBool("enemyDie", true);
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
     
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (enemyAnimator.GetBool("isEnemyAttacked"))
        {
            if (col.gameObject.name == "akai_e_espiritu")
            {
                col.gameObject.GetComponent<CharacterController>().TakeDamage(10);
            }
        }   
    }

    public void ZombieScreamAnimFinish()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(3f);
        enemyAnimator.SetBool("isEnemyRoaring", false);
        enemyAnimator.SetBool("isEnemyRunning", true);
        isEnemyRoared = true;
    }

    public void ZombieScreamAnimStart()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
    }

    public void StartAttackAnim()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
    }

    public void FinishAttackAnim()
    {
        enemyAnimator.SetBool("isEnemyAttacked", false);
        GetComponent<EnemyMovementAI>().SetEnemySpeed(2f);
    }

    public void StartWalkingAnim()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(2f);
    }

    public void StartReactAnim()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
    }

    public void EndReactAnim()
    {
        enemyAnimator.SetBool("enemyReact", false);
    }
    public void ZombieDieAnimEnd()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponentInChildren<CapsuleCollider>().enabled = false;
    }


}
