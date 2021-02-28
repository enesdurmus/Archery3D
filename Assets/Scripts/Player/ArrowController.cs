using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 24f;
    [SerializeField] private int arrowPower = 10;
    [SerializeField] private float slowFactor = 0.05f;
    private GameObject arrowOutPos;
    private Rigidbody physic;
    private Vector3 direction;
    private bool isArrowShooted = false;
    private RaycastHit hit;
    private GameObject mainCam;
    AudioSource[] audios;


    int counter = 0;

    private void Start()
    {
        audios = GetComponents<AudioSource>();
        arrowOutPos = GameObject.FindGameObjectWithTag("ArrowPosTag");
        physic = GetComponent<Rigidbody>();
        mainCam = Camera.main.gameObject;
    }

    private void Update()
    {
        if (isArrowShooted)
            AddForceToArrow();

        transform.forward = Vector3.Slerp(transform.forward, physic.velocity.normalized, 0.1f);
    }

    void AddForceToArrow()
    {
        physic.AddForce(direction * arrowSpeed * Time.deltaTime);

        if (arrowSpeed > 0)
        {
            arrowSpeed -= slowFactor;
        }
    }

    public void InputUpdates(float attackPower)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Physics.Raycast(new Ray(mainCam.transform.position, ray.direction), out hit);
        direction = (hit.point - arrowOutPos.transform.position).normalized;
        isArrowShooted = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;

        if (hit.transform.root.gameObject.CompareTag("Enemy") && hit.transform.root.gameObject.GetComponent<EnemyController>().GetHealt() == 10)
        {
            audios[2].Play();
            GetComponent<CameraTrackArrow>().enabled = true;
            hit.transform.root.GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
            GetComponent<CameraTrackArrow>().TrackArrow();
        }
    }

    IEnumerator DestroyArrow(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(counter++);
        Debug.Log(col.transform.tag);

        if (arrowSpeed > 0)
        {
            StickArrow(col);

            if (col.transform.root.gameObject.CompareTag("Enemy"))
            {
                if (col.transform.CompareTag("ZombieHead"))
                    arrowPower = 100;

                audios[1].Play();
                audios[2].Stop();

                col.transform.root.gameObject.GetComponent<BloodSplash>().Splash(transform.position + transform.forward.normalized * 2);

                if (col.transform.root.gameObject.GetComponent<EnemyController>().TakeDamage(arrowPower))
                {
                    col.transform.root.gameObject.GetComponent<EnemyController>().AddForceToBody(direction + new Vector3(0f, 30f, 0f).normalized);
                    GetComponent<CameraTrackArrow>().ExitTrackArrow(col.transform.root.gameObject.transform.Find("ZombieSlowPos").gameObject);
                }
            }
            else
            {
                GetComponent<CameraTrackArrow>().ExitTrackArrow();
                audios[0].Play();
            }
        }

        StartCoroutine(DestroyArrow(3f));
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "ArrowTrigger")
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }

    private void StickArrow(Collision col)
    {
        arrowSpeed = 0;
        physic.detectCollisions = false;
        physic.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        physic.isKinematic = true;
        isArrowShooted = false;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.SetParent(col.transform);
    }
}
