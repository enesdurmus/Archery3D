using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject SpawnEffectPrefab;

    private GameObject player;
    private Animator enemyAnimator;
    private GameObject gameMode;
    private bool isEnemyRoared = false;
    private int currentHealt;
    private float speed, runSpeed = 90f, walkSpeed = 40f;
    private bool isDead = false;
    private GameObject spawnEffect;

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
        allColliders = GetComponentsInChildren<Collider>();
        allRigidBodies = GetComponentsInChildren<Rigidbody>();
        mainRigidBody = GetComponent<Rigidbody>();
        DoRagdoll(false);
        gameMode = GameObject.FindGameObjectWithTag("GameMode");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAnimator = GetComponent<Animator>();

    }

    private void Start()
    {
        spawnEffect = Instantiate(SpawnEffectPrefab, transform.position, Quaternion.identity);
        currentHealt = maxHealt;
        healtBar.SetMaxHealt(maxHealt);
        attackPower = 10f;
        speed = walkSpeed;
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

       // foreach (Collider col in allColliders)
      //      col.enabled = isRagdoll;

      //  mainRigidBody.isKinematic = isRagdoll;
       // mainCollider.enabled = !isRagdoll;

        GetComponent<Rigidbody>().useGravity = !isRagdoll;

    }
    public bool TakeDamage(int damage)
    {
        currentHealt -= damage;
        audios[4].Play();
        healtBar.SetHealt(currentHealt);
        GetComponent<Animator>().SetBool("enemyReact", true);
        if (currentHealt <= 0)
        {
            isDead = true;
            mainRigidBody.isKinematic = false;
            DoRagdoll(true);
            EnemyDie();
        }

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
        speed = 0f;
        GetComponent<EnemyMovementAI>().SetEnemySpeed(speed);
        GetComponent<NavMeshAgent>().enabled = false;
        transform.Find("Canvas").Find("HealtBar").gameObject.SetActive(false);
        audios[1].Stop();
        audios[2].Stop();
        StartCoroutine(DestroyEnemy(5f));
    }

    public void AddForceToBody(Vector3 direction)
    {
        foreach (Rigidbody rb in allRigidBodies)
            rb.AddForce(direction * 25f, ForceMode.Impulse);
    }

    public void ZombieScreamAnimFinish()
    {
        speed = runSpeed;
        GetComponent<EnemyMovementAI>().SetEnemySpeed(speed);
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
        speed = 0f;
        GetComponent<EnemyMovementAI>().SetEnemySpeed(speed);
    }

    public void StartAttackAnim()
    {
        speed = 0f;
        GetComponent<EnemyMovementAI>().SetEnemySpeed(speed);
        audios[2].Pause();
        audios[3].Play();
    }

    public void FinishAttackAnim()
    {
        if (!(Vector3.Distance(player.transform.position, transform.position) <= 1.5f))
        {
            speed = runSpeed;
            GetComponent<EnemyMovementAI>().SetEnemySpeed(speed);
        }
        audios[2].UnPause();
        enemyAnimator.SetBool("isEnemyAttacked", false);
    }

    public void StartReactAnim()
    {
        GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
    }

    public void EndReactAnim()
    {
        enemyAnimator.SetBool("enemyReact", false);
        GetComponent<EnemyMovementAI>().SetEnemySpeed(speed);

    }
    public void ZombieDieAnimEnd()
    {
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }

    public void FinishSpawnAnim()
    {
        Destroy(spawnEffect.gameObject);
        speed = walkSpeed;
        GetComponent<EnemyMovementAI>().SetEnemySpeed(speed);
    }
}
