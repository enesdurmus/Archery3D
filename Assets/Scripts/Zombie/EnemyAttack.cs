using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = transform.root.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyAnimator != null)
            if (enemyAnimator.GetBool("isEnemyAttacked"))
                if (other.gameObject.name == "akai_e_espiritu")
                    other.gameObject.GetComponent<PlayerController>().TakeDamage(10);
    }
}
