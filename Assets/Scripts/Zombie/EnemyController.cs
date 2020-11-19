using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnimator;
    private bool isEnemyRoared = false;
    [SerializeField] private GameObject player;

    public float attackPower {get; set;}

    public HealtBar healtBar;
    public int maxHealt = 100;
    private int currentHealt;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        currentHealt = maxHealt;
        healtBar.SetMaxHealt(maxHealt);
        attackPower = 10f;
    }

    void Update()
    {
        GetComponent<EnemyMovementAI>().handleMovement();

        if (currentHealt > 0)
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
   
    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healtBar.SetHealt(currentHealt);
        EnemyDie();

        if(currentHealt != 0) {
            enemyAnimator.SetBool("enemyReact", true);
        }
        
        Debug.Log(currentHealt);
    }

    public void EnemyDie()
    {
        if(currentHealt == 0)
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
