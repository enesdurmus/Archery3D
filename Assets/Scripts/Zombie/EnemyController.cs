using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private GameObject player;
    private Animator enemyAnimator;
    private GameObject gameMode;

    public HealtBar healtBar;

    public float attackPower { get; set; }
    public int maxHealt = 100;

    private bool isEnemyRoared = false;
    private int currentHealt;
    private int isHit = 0;


    void Start()
    {
        gameMode = GameObject.FindGameObjectWithTag("GameMode");
        player = GameObject.FindGameObjectWithTag("Player");
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

    IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void AttackControl()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 1.5f && enemyAnimator.GetBool("isEnemyAttacked") == false)
        {
            enemyAnimator.SetBool("isEnemyAttacked", true);
        }

        if (!isEnemyRoared)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= 7f)
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
        if (currentHealt != 0)
        {
            enemyAnimator.SetBool("enemyReact", true);
        }

        Debug.Log(currentHealt);
    }

    public void EnemyDie()
    {
        if (currentHealt == 0)
        {
            gameMode.GetComponent<GameControl>().SetKillEnemyCount();
            enemyAnimator.SetBool("enemyDie", true);
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (enemyAnimator != null)
        {
            if (enemyAnimator.GetBool("isEnemyAttacked"))
            {
                if (col.gameObject.name == "akai_e_espiritu")
                {
                    if (isHit == 0)
                    {
                        col.gameObject.GetComponent<PlayerController>().TakeDamage(10);
                        isHit = 1;
                    }
                }
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
        if (!(Vector3.Distance(player.transform.position, transform.position) <= 1.5f))
        {
            GetComponent<EnemyMovementAI>().SetEnemySpeed(3f);
        }
        enemyAnimator.SetBool("isEnemyAttacked", false);
        isHit = 0;
    }

    public void StartReactAnim()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
    }

    public void EndReactAnim()
    {
        enemyAnimator.SetBool("enemyReact", false);
        GetComponent<EnemyMovementAI>().SetEnemySpeed(1f);

    }
    public void ZombieDieAnimEnd()
    {
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }


}
