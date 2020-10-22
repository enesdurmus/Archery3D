using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject Bow = null;

    [SerializeField] private GameObject arrowTrigger = null;

    [SerializeField] private RuntimeAnimatorController whileAimingAnimatorController = null, simpleAnimatorController = null;

    private Animator CharacterAnimator;
    private bool isMouseClicked;
    private bool drawControl = false;
 
    private void Start()
    {
        CharacterAnimator = GetComponent<Animator>();
        Bow.GetComponent<BowController>().CreateArrow();
    }


    public void FinishShootAnim()
    {
        CharacterAnimator.SetBool("ShootArrow", false);
        CharacterAnimator.runtimeAnimatorController = simpleAnimatorController;
        drawControl = false;
        arrowTrigger.SetActive(false);
    }

    private void DrawBow()
    {
        Bow.GetComponent<BowController>().DrawBow();
    }

    public void HandleShootArrow(){
        isMouseClicked = GetComponent<PlayerInput>().GetMouseClickInf();
        if (isMouseClicked){
            CharacterAnimator.runtimeAnimatorController = whileAimingAnimatorController;
            if (CharacterAnimator.GetBool("ShootArrow") == false){ 
                DrawBow();
                drawControl = true;
            }
        }
        else{
            if(drawControl == true) {
                arrowTrigger.SetActive(true);
                Bow.GetComponent<BowController>().shootArrow(20f);
                CharacterAnimator.SetBool("ShootArrow", true);
            }
        }           
    }
}
