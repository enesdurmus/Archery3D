using UnityEngine;

public class PickUp : MonoBehaviour
{
    private RaycastHit hit;
    private float distance = 2f;
    private GameObject character;
    
    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Ray ray = new Ray(character.transform.position + new Vector3(0f,0.5f,0f), Camera.main.transform.forward);
        Physics.Raycast(ray, out hit, distance);

        Debug.DrawLine(ray.origin, hit.point);
        // press f to pick up arrow.
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hit.transform.gameObject.tag == "ArrowTag")
            {
                hit.transform.gameObject.SetActive(false);
                hit.transform.gameObject.GetComponent<ArrowController>().ResetArrow();
            }
            
        }
        
    }
}
