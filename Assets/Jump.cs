using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] string jumpAnimParameter = "isJumping";

    Animator anim;

    Vector3 moveDirection;

    CharacterController controller;

    float gravity = -9.81f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            DoJump();

        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void DoJump()
    {
        moveDirection.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        controller.Move(moveDirection * Time.deltaTime);
        anim.SetBool(jumpAnimParameter, true);
    }

    public void JumpFinish()
    {
        anim.SetBool(jumpAnimParameter, false);
    }
}

