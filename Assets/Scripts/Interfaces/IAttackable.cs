using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    float attackPower { get; set; }

    void Attack();

    void AttackControl();
}
