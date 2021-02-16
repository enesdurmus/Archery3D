using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private GameObject player;
    private Animator enemyAnimator;
    private GameObject gameMode;
    private bool isEnemyRoared = false;
    private int currentHealt;
    private int isHit = 0;
    private bool isDead = false;

    private Collider mainCollider;
    private Collider[] allColliders;
    private Rigidbody[] allRigidBodies;
    private Rigidbody mainRigidBody;

    public HealtBar healtBar;

    public float attackPower { get; set; }
    public int maxHealt = 100;



    void Awake()
    {
        mainCollider = GetComponent<Collider>();
        allColliders = GetComponentsInChildren<Collider>();
        allRigidBodies = GetComponentsInChildren<Rigidbody>();
        mainRigidBody = GetComponent<Rigidbody>();
        DoRagdoll(false);
        gameMode = GameObject.FindGameObjectWithTag("GameMode");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAnimator = GetComponent<Animator>();
        currentHealt = maxHealt;
        healtBar.SetMaxHealt(maxHealt);
        attackPower = 10f;
    }

    void Update()
    {
        if(!isDead) GetComponent<EnemyMovementAI>().handleMovement();

        if (currentHealt > 0)
        {
            AttackControl();
        }
    }

    IEnumerator DestroyEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
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

    public void DoRagdoll(bool isRagdoll)
    {
        foreach (Collider col in allColliders)
            col.enabled = isRagdoll;
        foreach (Rigidbody rb in allRigidBodies)
            rb.isKinematic = !isRagdoll;

        mainRigidBody.isKinematic = isRagdoll;
        mainCollider.enabled = !isRagdoll;

        GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }
    public bool TakeDamage(int damage)
    {
        currentHealt -= damage;
        healtBar.SetHealt(currentHealt);
        if (currentHealt == 0)
        {
            isDead = true;
            mainRigidBody.isKinematic = false;
            EnemyDie();
            DoRagdoll(true);
            DestroyEnemy(10f);
        }
        Debug.Log(currentHealt);

        return isDead;
    }

    public void EnemyDie()
    {
        gameMode.GetComponent<GameControl>().SetKillEnemyCount();
        GetComponent<EnemyMovementAI>().enabled = false;
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
        GetComponent<NavMeshAgent>().enabled = false;
    }

    public void AddForceToBody(Vector3 direction)
    {
        foreach (Rigidbody rb in allRigidBodies)
            rb.AddForce(direction * 50f, ForceMode.Impulse);
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
