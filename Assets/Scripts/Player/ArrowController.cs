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

        if (hit.transform.tag == "Enemy" && hit.transform.gameObject.GetComponent<EnemyController>().GetHealt() == 10)
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

    private void OnCollisionEnter(Collision col)
    {
        if (arrowSpeed > 0)
        {
            if (col.gameObject.tag == "Enemy")
            {
                audios[1].Play();

                col.gameObject.GetComponent<BloodSplash>().Splash(transform.position + transform.forward.normalized * 2);

                if (col.gameObject.GetComponent<EnemyController>().TakeDamage(arrowPower))
                {
                    col.gameObject.GetComponent<EnemyController>().AddForceToBody(direction + new Vector3(0f, 30f, 0f).normalized);
                    GetComponent<CameraTrackArrow>().ExitTrackArrow(col.gameObject.transform.Find("ZombieSlowPos").gameObject);
                }
            }
            else
            {
                GetComponent<CameraTrackArrow>().ExitTrackArrow();
                audios[0].Play();
            }

        }

        StartCoroutine(DestroyArrow(3f));
        StickArrow(col);
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
        physic.detectCollisions = false;
        physic.isKinematic = true;
        isArrowShooted = false;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.SetParent(col.transform);
    }
}
