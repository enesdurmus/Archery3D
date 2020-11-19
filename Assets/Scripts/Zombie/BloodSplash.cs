using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    [SerializeField] private GameObject blood;

    public void Splash(Vector3 hitPoint)
    {
        Instantiate(blood, hitPoint, Quaternion.identity);
    }
}
