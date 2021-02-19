﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] [Range(0.0f, 3.0f)] private float CharacterMovementSpeed, CharacterRotationSpeed;

    public float attackPower {get; set;}
    private Animator CharacterAnimator;
    public HealtBar healtBar;
    private int maxHealt = 100;
    private int currentHealt;

    void Start()
    {
        currentHealt = maxHealt;
        healtBar.SetMaxHealt(maxHealt);
        attackPower = 10f;
        CharacterAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        GetComponent<PlayerMovement>().FindMaxSpeed();
        GetComponent<ShootArrow>().HandleShootArrow();
    }

    private void FixedUpdate()
    {
        GetComponent<PlayerMovement>().HandleMovement();
    }

    private void LateUpdate()
    {
        GetComponent<CameraController>().handleCameraMovement();

    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healtBar.SetHealt(currentHealt);
        CharacterAnimator.SetBool("ReactParam", true);
    }
    public void ReactAnimEnd()
    {
        CharacterAnimator.SetBool("ReactParam", false);
    }
}