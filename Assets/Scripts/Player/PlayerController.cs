using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] [Range(0.0f, 3.0f)] private float CharacterMovementSpeed, CharacterRotationSpeed;

    public float attackPower {get; set;}
    private Animator CharacterAnimator;
    public HealtBar healtBar;
    private int maxHealt = 100;
    private int currentHealt;
    private RaycastHit hit;
    void Start()
    {
        currentHealt = maxHealt;
        healtBar.SetMaxHealt(maxHealt);
        attackPower = 10f;
        CharacterAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        GetComponent<ShootArrow>().HandleShootArrow();
        GetComponent<PlayerMovement>().HandleMovement();
        GetComponent<CameraController>().handleCameraMovement();

    }

    private void LateUpdate()
    {
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