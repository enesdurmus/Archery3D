using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private GameObject arrowOutPos;
    private Rigidbody physic;
    private Vector3 direction;
    private float arrowSpeed = 18f;
    private bool isArrowShooted = false;
    private RaycastHit hit, hit2;
    private int arrowPower = 0;

    private void Start()
    {
        arrowOutPos = GameObject.FindGameObjectWithTag("ArrowPosTag");
        physic = GetComponent<Rigidbody>();
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
            arrowSpeed -= 0.01f;
        }
    }

    public void InputUpdates(float attackPower)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Physics.Raycast(new Ray(arrowOutPos.transform.position + new Vector3(0.35f, 0.3f, 0f), ray.direction), out hit);
        direction = (hit.point - arrowOutPos.transform.position).normalized;
        arrowPower = 10;
        isArrowShooted = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;

        if (hit.transform.tag == "Enemy")
        {
            Debug.Log("naber");
            GetComponent<CameraTrackArrow>().enabled = true;
            GetComponent<CameraTrackArrow>().TrackArrow();
        }
    }

    public void ResetArrow()
    {
        arrowSpeed = 1f;
        isArrowShooted = false;
        GetComponent<CapsuleCollider>().isTrigger = true;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CameraTrackArrow>().enabled = false;
    }

    IEnumerator DestroyArrow(float time)
    {
        GetComponent<CameraTrackArrow>().ExitTrackArrow();
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (arrowSpeed > 0)
        {
            if (col.gameObject.name == "zombie")
            {
                col.gameObject.GetComponent<BloodSplash>().Splash(transform.position);
                col.gameObject.GetComponent<EnemyController>().TakeDamage(arrowPower);
                GetComponent<CameraTrackArrow>().ExitTrackArrow();
            }
        }
        StickArrow(col);
        //StartCoroutine(DestroyArrow(2f));
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
        Debug.Log("giriyormu");
        physic.isKinematic = true;
        isArrowShooted = false;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.SetParent(col.transform);
    }
}
