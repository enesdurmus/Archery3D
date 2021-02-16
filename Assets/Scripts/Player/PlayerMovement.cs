using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController control;

    public Transform cam;

    [SerializeField] private float walkSpeed = 1, sprintSpeed = 3;

    [SerializeField] private float turnSmoothTime = 0.1f;

    Vector3 moveDirection;

    float gravity = -9.81f;

    private float speed = 0, maxSpeed;

    private float vertical = 0, horizontal = 0;

    private Animator CharacterAnimator;

    float turnSmoothVelocity;

    void Start()
    {
        CharacterAnimator = GetComponent<Animator>();
    }

    public void HandleMovement()
    {
        float[] movementInputs = GetComponent<InputController>().GetMovementInputs();
        vertical = movementInputs[0];
        horizontal = movementInputs[1];

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        FindMaxSpeed();


        if (CharacterAnimator.runtimeAnimatorController.name == "CharacterAnimatorControllerSimple")
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                HandleMoveSpeed();

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                control.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else HandleStopSpeed();

            CharacterAnimator.SetFloat("speed", speed);
        }
        else
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                HandleMoveSpeed();

                control.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else HandleStopSpeed();

            CharacterAnimator.SetFloat("vertical", vertical);
            CharacterAnimator.SetFloat("horizontal", horizontal);
        }

        moveDirection.y += gravity * Time.deltaTime;
        control.Move(moveDirection * Time.deltaTime);

    }

    private void FindMaxSpeed()
    {
        if (GetComponent<InputController>().GetSprintInput()) maxSpeed = sprintSpeed;
        else maxSpeed = walkSpeed;
    }

    private void HandleMoveSpeed()
    {
        if (speed < maxSpeed) speed += 0.1f;

        else if (speed > maxSpeed) speed -= 0.1f;
    }

    private void HandleStopSpeed()
    {
        if (speed > 0)
        {
            speed -= 0.12f;
        }
    }
}
