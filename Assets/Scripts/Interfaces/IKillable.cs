using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    float Healt { get; set; }

    void TakeDamage(float damage);
}
