using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private GameObject arrowOutPos;
    private Rigidbody physic;
    private Vector3 direction;
    private float arrowSpeed = 2f;
    private bool isArrowShooted = false;
    private RaycastHit hit;
    private int arrowPower = 0;

    private void Start()
    {
        arrowOutPos = GameObject.FindGameObjectWithTag("ArrowPosTag");
        physic = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isArrowShooted)
        {
            AddForceToArrow();
        }

        transform.forward = Vector3.Slerp(transform.forward, physic.velocity.normalized, 0.1f);
    }

    void AddForceToArrow()
    {
        physic.AddForce(direction * arrowSpeed);
        Debug.Log("alooo");
        GetComponent<CameraTrackArrow>().TrackArrow();


        if (arrowSpeed > 0)
        {
            arrowSpeed -= 0.01f;
        }
    }

    public void InputUpdates(float attackPower)
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Physics.Raycast(new Ray(arrowOutPos.transform.position + new Vector3(0.3f, 0.2f, 0f), ray.direction), out hit);
        direction = (hit.point - arrowOutPos.transform.position).normalized;
        arrowPower = 10;
        isArrowShooted = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResetArrow()
    {
        arrowSpeed = 2f;
        isArrowShooted = false;
        GetComponent<CapsuleCollider>().isTrigger = true;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (arrowSpeed > 0)
        {
            if (col.gameObject.name == "zombie")
            {
                col.gameObject.GetComponent<BloodSplash>().Splash(transform.position);
                col.gameObject.GetComponent<EnemyController>().TakeDamage(arrowPower);
            }
        }
        arrowSpeed = 0f;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "ArrowTrigger")
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
}
