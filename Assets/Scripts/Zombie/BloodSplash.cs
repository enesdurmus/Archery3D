using System.Collections;
using UnityEngine;


public class BloodSplash : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;

    [SerializeField] private Vector3 offSet;


    public void Splash(Vector3 hitPoint)
    {
        Instantiate(bloodPrefab, hitPoint, Quaternion.identity);
        // blood.GetComponent<ParticleSystem>().Play();
    }

}