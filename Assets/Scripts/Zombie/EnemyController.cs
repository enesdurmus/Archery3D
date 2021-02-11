using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator enemyAnimator;
    private bool isEnemyRoared = false;
    [SerializeField] private GameObject player;

    public float attackPower { get; set; }

    public HealtBar healtBar;
    public int maxHealt = 100;
    private int currentHealt;
    private int isHit = 0;

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

        else
        {
            enemyAnimator.SetBool("isEnemyAttacked", false);
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
            enemyAnimator.SetBool("enemyDie", true);
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
            Debug.Log("girdim, aq");
        }
    }

    void OnCollisionEnter(Collision col)
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
        GetComponent<EnemyMovementAI>().SetEnemySpeed(3f);
        isHit = 0;
    }

    public void StartWalkingAnim()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(1f);
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
