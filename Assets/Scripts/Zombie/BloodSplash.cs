﻿using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;

    [SerializeField] private Vector3 offSet;


    private GameObject blood;

    public void Splash(Vector3 hitPoint)
    {
        blood = Instantiate(bloodPrefab, hitPoint - offSet, Quaternion.identity);
        blood.GetComponent<ParticleSystem>().Play();
    }
}
