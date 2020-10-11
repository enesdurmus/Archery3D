using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float vertical = 0, horizontal = 0;
    private Rigidbody physic;
    private Animator CharacterAnimator;

    void Start()
    {
        physic = GetComponent<Rigidbody>();
        CharacterAnimator = GetComponent<Animator>();
    }

    public void handleMovement(float CharacterMovementSpeed, float CharacterRotationSpeed)
    {
        float[] movementInputs = GetComponent<PlayerInput>().GetMovementInputs();
        vertical = movementInputs[0];
        horizontal = movementInputs[1];

        Vector3 vectorX = transform.forward * vertical * CharacterMovementSpeed;
        vectorX.y = physic.velocity.y;
        Vector3 vectorZ = transform.right * horizontal * CharacterRotationSpeed;

        physic.velocity = vectorX + vectorZ;

        CharacterAnimator.SetFloat("HorizontalAnim", horizontal);
        CharacterAnimator.SetFloat("VerticalAnim", vertical);
    }

  /*  public void HandleRun()
    {
        if (CharacterAnimator.runtimeAnimatorController.name == "CharacterAnimatorControllerSimple")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                CharacterAnimator.SetBool("RunAnim", true);
                CharacterMovementSpeed *= 2;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                CharacterAnimator.SetBool("RunAnim", false);
                CharacterMovementSpeed /= 2;
            }
        }
    }

    public void handleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterAnimator.SetBool("JumpAnim", true);
            physic.AddForce(0, 500, 0);
        }
    }*/
}
