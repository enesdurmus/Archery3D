using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
    private GameObject arrowOutPos;
    private Rigidbody physic;
    private Vector3 forceVector;
    private float arrowSpeed = 5f;
    private bool isArrowShooted = false;
    private RaycastHit hit;
    private float arrowPower = 0;

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
        transform.forward = Vector3.Slerp(transform.forward, physic.velocity.normalized, Time.deltaTime);
    }

    void AddForceToArrow()
    {     
        Vector3 direction = hit.point + new Vector3(0f,0.5f,0f) - arrowOutPos.transform.position;

        physic.AddForce(direction.normalized * arrowSpeed);

        if (arrowSpeed > 0) {
            arrowSpeed -= 0.01f;
        }    
    }

    public void InputUpdates(float attackPower)
    {
        Ray ray = new Ray(arrowOutPos.transform.position, transform.forward);
        Physics.Raycast(ray, out hit);
        arrowPower = attackPower;
        isArrowShooted = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResetArrow()
    {
        arrowSpeed = 0.6f;
        isArrowShooted = false;
        GetComponent<CapsuleCollider>().isTrigger = true;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if(arrowSpeed > 0) {
            if (col.gameObject.name == "mutant")
            {
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
