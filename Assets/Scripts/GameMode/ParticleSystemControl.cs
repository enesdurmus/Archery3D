using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemControl : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem[] ps1;


    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (ps != null)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
