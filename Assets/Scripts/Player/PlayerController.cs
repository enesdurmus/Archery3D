using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] [Range(0.0f, 3.0f)] private float CharacterMovementSpeed, CharacterRotationSpeed;

    [SerializeField] private RuntimeAnimatorController simpleAnimatorController = null;
    public float attackPower {get; set;}
    private Animator CharacterAnimator;
    public HealtBar healtBar;
    private int maxHealt = 100;
    private int currentHealt;
    private bool isDead = false;

    void Start()
    {
        currentHealt = maxHealt;
        healtBar.SetMaxHealt(maxHealt);
        attackPower = 10f;
        CharacterAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead) {
            currentHealt -= damage;
            healtBar.SetHealt(currentHealt);
            if (CharacterAnimator.runtimeAnimatorController.name == "CharacterAnimatorControllerAiming")
                CharacterAnimator.runtimeAnimatorController = simpleAnimatorController;
            if (currentHealt == 0)
            {
                isDead = true;
                GetComponent<CharacterController>().enabled = false;
                GetComponent<CameraController>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<ShootArrow>().enabled = false;
                CharacterAnimator.SetBool("isDead", true);
            }
            else
            {
                CharacterAnimator.SetBool("ReactParam", true);
                GetComponent<ShootArrow>().ResetShoot();

            }
        }    
    }

    public void ReactAnimEnd()
    {
        CharacterAnimator.SetBool("ReactParam", false);
    }

    public void FinishDeadAnim()
    {
        CharacterAnimator.SetBool("isDead", false);
    }
}