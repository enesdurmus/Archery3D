using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject Bow = null;

    [SerializeField] private GameObject arrowTrigger = null;

    [SerializeField] private RuntimeAnimatorController whileAimingAnimatorController = null, simpleAnimatorController = null;

    private Animator CharacterAnimator;
    private bool isMouseClicked;
    private bool drawControl = false;
    private bool isArrowCreated = false;
    private bool isArrowReleased = true;
    private bool canDrawAgain = true;
    private bool isDrawSoundPlayed = false;
    AudioSource[] audios;


    private void Start()
    {
        audios = GetComponents<AudioSource>();
        CharacterAnimator = GetComponent<Animator>();
    }

    private void DrawBow()
    {
        Bow.GetComponent<BowController>().DrawBow();
        if (!isDrawSoundPlayed)
            audios[0].Play(); isDrawSoundPlayed = true;
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
                    CharacterAnimator.runtimeAnimatorController = whileAimingAnimatorController;
                    isArrowCreated = true;
                    isArrowReleased = false;
                }
                if (CharacterAnimator.GetBool("ShootArrow") == false)
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
                    isDrawSoundPlayed = false;
                    audios[1].Play();
                    arrowTrigger.SetActive(true);
                    Bow.GetComponent<BowController>().ShootArrow(10f);
                    CharacterAnimator.SetBool("ShootArrow", true);
                    isArrowCreated = false;
                }
            }
        }
    }

    public void StartShootAnim()
    {
        canDrawAgain = false;
    }

    public void FinishShootAnim()
    {
        canDrawAgain = true;
        CharacterAnimator.SetBool("ShootArrow", false);
        CharacterAnimator.runtimeAnimatorController = simpleAnimatorController;
        drawControl = false;
        arrowTrigger.SetActive(false);
        isArrowReleased = true;
    }

}
