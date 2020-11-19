using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] [Range(0.0f, 3.0f)] private float CharacterMovementSpeed, CharacterRotationSpeed;

    public float attackPower {get; set;}
    public float Healt {get; set;}
    private Animator CharacterAnimator;

    void Start()
    {
        Healt = 100f;
        attackPower = 10f;
        CharacterAnimator = GetComponent<Animator>();

    }


    private void Update()
    {
        GetComponent<CameraController>().handleCameraMovement();
        CharacterMovementSpeed = GetComponent<PlayerMovement>().HandleRun(CharacterMovementSpeed);
        GetComponent<PlayerMovement>().handleJump();
        GetComponent<ShootArrow>().HandleShootArrow();
    }

    private void FixedUpdate()
    {
        GetComponent<PlayerMovement>().handleMovement(CharacterMovementSpeed, CharacterRotationSpeed);
    }
    
    public void TakeDamage(float damage)
    {
        Healt -= damage;
        CharacterAnimator.SetBool("ReactParam", true);
    }
    public void ReactAnimEnd()
    {
        CharacterAnimator.SetBool("ReactParam", false);
    }
}