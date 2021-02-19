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
    AudioSource[] audios;

    public HealtBar healtBar;
    public float attackPower { get; set; }
    public int maxHealt = 100;



    void Awake()
    {
        audios = GetComponents<AudioSource>();
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
        GetComponent<Animator>().enabled = !isRagdoll;


        foreach (Rigidbody rb in allRigidBodies)
            rb.isKinematic = !isRagdoll;

        foreach (Collider col in allColliders)
            col.enabled = isRagdoll;

        mainCollider.enabled = !isRagdoll;
        mainRigidBody.isKinematic = isRagdoll;

        GetComponent<Rigidbody>().useGravity = !isRagdoll;

    }
    public bool TakeDamage(int damage)
    {
        currentHealt -= damage;
        healtBar.SetHealt(currentHealt);
        GetComponent<Animator>().SetBool("enemyReact", true);
        if (currentHealt == 0)
        {
            isDead = true;
            mainRigidBody.isKinematic = false;
            DoRagdoll(true);
            EnemyDie();
            DestroyEnemy(7f);
        }
        Debug.Log(currentHealt);

        return isDead;
    }

    public int GetHealt()
    {
        return currentHealt;
    }

    public void EnemyDie()
    {
        gameMode.GetComponent<GameControl>().SetKillEnemyCount();
        GetComponent<EnemyMovementAI>().enabled = false;
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
        GetComponent<NavMeshAgent>().enabled = false;
        transform.Find("Canvas").Find("HealtBar").gameObject.SetActive(false);
        audios[1].Stop();
        audios[2].Stop();
        audios[4].Play();
    }

    public void AddForceToBody(Vector3 direction)
    {
        foreach (Rigidbody rb in allRigidBodies)
            rb.AddForce(direction * 25f, ForceMode.Impulse);
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
        audios[0].Stop();
        audios[2].Play();
    }

    public void ZombieScreamAnimStart()
    {
        audios[0].Play(0);
        audios[1].Stop();
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
    }

    public void StartAttackAnim()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
        audios[2].Pause();
        audios[3].Play();
    }

    public void FinishAttackAnim()
    {
        if (!(Vector3.Distance(player.transform.position, transform.position) <= 1.5f))
        {
            GetComponent<EnemyMovementAI>().SetEnemySpeed(3f);
        }
        audios[2].UnPause();
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
