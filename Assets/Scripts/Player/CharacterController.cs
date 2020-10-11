using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 3.0f)] private float CharacterMovementSpeed, CharacterRotationSpeed;

    [SerializeField]
    private GameObject characterCamera = null, crossHair = null;

    [SerializeField]
    private GameObject Bow = null;

    [SerializeField]
    private RuntimeAnimatorController whileAimingAnimatorController = null, simpleAnimatorController = null;


    private Rigidbody physic;

    private float vertical = 0, horizontal = 0;
    private float verticalMouse = 0, horizontalMouse = 0;

    private Animator CharacterAnimator;
    private Transform iskelet;
    private bool drawControl = false;

    public float attackPower {get; set;}
    public float Healt {get; set;}

    void Start()
    {
        Healt = 100f;
        attackPower = 10f;

        physic = GetComponent<Rigidbody>();
        CharacterAnimator = GetComponent<Animator>();

    }
  

    private void Update()
    {
        HandleRun();

        handleJump();
 
        CharacterAnimator.SetFloat("HorizontalAnim", horizontal);
        CharacterAnimator.SetFloat("VerticalAnim", vertical);

    }

    private void FixedUpdate()
    {
        handleMovement();
    }


    

    public void handleMovement()
    {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        Vector3 vectorX = transform.forward * vertical * CharacterMovementSpeed;
        vectorX.y = physic.velocity.y;
        Vector3 vectorZ = transform.right * horizontal * CharacterRotationSpeed;

        physic.velocity = vectorX + vectorZ;
    }

    public void HandleRun()
    {
        if (CharacterAnimator.runtimeAnimatorController != whileAimingAnimatorController) {

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
    }


    public void TakeDamage(float damage)
    {
        Healt -= damage;
    }
}