using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 18f;
    [SerializeField] private int arrowPower = 10;
    [SerializeField] private float slowFactor = 0.01f;
    private GameObject arrowOutPos;
    private Rigidbody physic;
    private Vector3 direction;
    private bool isArrowShooted = false;
    private RaycastHit hit, hit2;
    private GameObject mainCam;

    private void Start()
    {
        arrowOutPos = GameObject.FindGameObjectWithTag("ArrowPosTag");
        physic = GetComponent<Rigidbody>();
        mainCam = Camera.main.gameObject;
    }

    private void FixedUpdate()
    {
        if (isArrowShooted)
        {
            AddForceToArrow();
        }
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

        if (hit.transform.tag == "Enemy")
        {
            GetComponent<CameraTrackArrow>().enabled = true;
            hit.transform.GetComponent<EnemyMovementAI>().SetEnemySpeed(0f);
            GetComponent<CameraTrackArrow>().TrackArrow();
        }
    }

    IEnumerator DestroyArrow(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.transform.name);

        if (arrowSpeed > 0)
        {
            if (col.gameObject.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyMovementAI>().SetEnemySpeed(3f);
                col.gameObject.GetComponent<BloodSplash>().Splash(transform.position);
                col.gameObject.GetComponent<EnemyController>().TakeDamage(arrowPower);
                GetComponent<CameraTrackArrow>().ExitTrackArrow(col.transform.Find("ZombieSlowPos").gameObject);
            }
        }
        StickArrow(col);
        StartCoroutine(DestroyArrow(10f));
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
        physic.isKinematic = true;
        isArrowShooted = false;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.SetParent(col.transform);
    }
}
