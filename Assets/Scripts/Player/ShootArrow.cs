using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject Bow;

    [SerializeField] private GameObject arrowTrigger;

    [SerializeField] private RuntimeAnimatorController whileAimingAnimatorController, simpleAnimatorController;

    private Animator characterAnimator;
    private bool isMouseClicked;
    private bool drawControl = false;
    private bool isArrowCreated = false;
    private bool isArrowReleased = true;
    private bool canDrawAgain = true;
    private bool isBowDrawed = false;
    AudioSource[] audios;


    private void Start()
    {
        audios = GetComponents<AudioSource>();
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleShootArrow();
    }
    private void DrawBow()
    {
        Bow.GetComponent<BowController>().DrawBow();
        if (!isBowDrawed)
        {
            audios[0].Play();
            isBowDrawed = true;
        }
    }

    public void HandleShootArrow()
    {
        isMouseClicked = GetComponent<InputController>().GetMouseClickInf();
        if (canDrawAgain)
        {
            if (isMouseClicked)
            {
                if (!isArrowCreated)
                {
                    Bow.GetComponent<BowController>().CreateArrow();
                    characterAnimator.runtimeAnimatorController = whileAimingAnimatorController;
                    isArrowCreated = true;
                    isArrowReleased = false;
                }
                if (characterAnimator.GetBool("ShootArrow") == false)
                {
                    DrawBow();
                    drawControl = true;
                }
            }
            else
            {
                if (drawControl == true && isArrowReleased == false)
                {
                    isArrowReleased = true;
                    isBowDrawed = false;
                    audios[1].Play();
                    arrowTrigger.SetActive(true);
                    Bow.GetComponent<BowController>().ShootArrow(10f);
                    characterAnimator.SetBool("ShootArrow", true);
                    isArrowCreated = false;
                }
            }
        }
    }

    public void ResetShoot()
    {
        if(characterAnimator.runtimeAnimatorController.name == "CharacterAnimatorControllerAiming")
            characterAnimator.SetBool("ShootArrow", false);
        drawControl = false;
        arrowTrigger.SetActive(false);
        isArrowReleased = true;
        canDrawAgain = true;
        isArrowCreated = false;
    }

    public void StartShootAnim()
    {
        canDrawAgain = false;
    }

    public void FinishShootAnim()
    {
        characterAnimator.SetBool("ShootArrow", false);
        characterAnimator.runtimeAnimatorController = simpleAnimatorController;
        drawControl = false;
        arrowTrigger.SetActive(false);
        isArrowReleased = true;
        canDrawAgain = true;
    }

    public void StartDrawArrow()
    {
        Bow.GetComponent<BowController>().StartDrawAnim();
    }
}
