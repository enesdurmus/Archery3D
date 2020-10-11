using UnityEngine;

public class CharacterController : MonoBehaviour, IMovable, IRunnable, IJumpable, IArcher, IKillable
{
    [SerializeField]
    [Range(0.0f, 3.0f)] private float CharacterMovementSpeed, CharacterRotationSpeed;

    [SerializeField]
    private GameObject characterCamera = null, crossHair = null;

    [SerializeField]
    private GameObject Bow = null;

    [SerializeField]
    private RuntimeAnimatorController whileAimingAnimatorController = null, simpleAnimatorController = null;

    private GameObject cam1, pos1, pos2;

    private Rigidbody physic;

    private float vertical = 0, horizontal = 0;
    private float verticalMouse = 0, horizontalMouse = 0;

    private Animator CharacterAnimator;
    private Transform iskelet;
    private GameObject[] arrowPool;
    private bool drawControl = false;

    public float attackPower {get; set;}
    public float Healt {get; set;}

    void Start()
    {
        Healt = 100f;
        attackPower = 10f;

        physic = GetComponent<Rigidbody>();
        CharacterAnimator = GetComponent<Animator>();
        iskelet = CharacterAnimator.GetBoneTransform(HumanBodyBones.Chest);
        cam1 = characterCamera.transform.Find("Main Camera").gameObject;
        pos1 = characterCamera.transform.Find("pos1").gameObject;
        pos2 = characterCamera.transform.Find("pos2").gameObject;
        arrowPool = Bow.GetComponent<BowController>().CreateArrow();

    }

    private void Update()
    {
        HandleRun();

        AttackControl();

        handleJump();
        

        if (drawControl) {

            CameraMoveWhileAiming();
            //Cam goes to close right of our character.
            cam1.transform.position = Vector3.Lerp(cam1.transform.position, pos2.transform.position, 0.015f);

            DrawBow();

            Attack();
        }
        else
        {
            CameraMoveSimple();
            cam1.transform.position = Vector3.Lerp(cam1.transform.position, pos1.transform.position, 0.015f);
        }

        CharacterAnimator.SetFloat("HorizontalAnim", horizontal);
        CharacterAnimator.SetFloat("VerticalAnim", vertical);

    }

    private void FixedUpdate()
    {
        handleMovement();
    }

    private void LateUpdate()
    {

        if (drawControl)
        {
           iskelet.rotation = iskelet.rotation * Quaternion.Euler(new Vector3(0, 0, verticalMouse));
        }
    }

    private void CameraMoveWhileAiming()
    {
        handleCameraMovement();
        crossHair.SetActive(true);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, horizontalMouse, transform.eulerAngles.z), 0.4f);
    }

    private void CameraMoveSimple()
    {
        handleCameraMovement();

        if (horizontal != 0 || vertical != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, horizontalMouse, transform.eulerAngles.z), 0.4f);
        }
    }

    private void handleCameraMovement()
    {
        crossHair.SetActive(false);

        verticalMouse += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * -150;
        horizontalMouse += Input.GetAxisRaw("Mouse X") * Time.deltaTime * 150;

        characterCamera.transform.rotation = Quaternion.Euler(verticalMouse, horizontalMouse, transform.eulerAngles.z);
        characterCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        verticalMouse = Mathf.Clamp(verticalMouse, -30, 10);
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

    public void finishJumpAnim()
    {
        CharacterAnimator.SetBool("JumpAnim", false);
    }

    //------------------------SHOOT---------------------------------//

    public void finishShootAnim()
    {
        CharacterAnimator.SetBool("isArrowShooted", false);
        CharacterAnimator.runtimeAnimatorController = simpleAnimatorController;
        drawControl = false;
    }

    public void DrawBow()
    {
        if (!CharacterAnimator.GetBool("isArrowShooted")){
            Bow.GetComponent<BowController>().DrawBow();
        }
        
    }

    public void Attack()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (drawControl)
            {
                if (!CharacterAnimator.GetBool("isArrowShooted"))
                {
                    Bow.GetComponent<BowController>().shootArrow(attackPower);
                    CharacterAnimator.SetBool("isArrowShooted", true);
                }
            }
        }
    }

    public void AttackControl()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CharacterAnimator.runtimeAnimatorController = whileAimingAnimatorController;
            drawControl = true;
        }
    }

    public void TakeDamage(float damage)
    {
        Healt -= damage;
    }
}
