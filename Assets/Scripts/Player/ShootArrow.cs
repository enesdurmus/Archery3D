using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private GameObject Bow = null;

    [SerializeField] private RuntimeAnimatorController whileAimingAnimatorController = null, simpleAnimatorController = null;

    private Animator CharacterAnimator;
    private bool isMouseClicked;
    private bool drawControl = false;
 
    private void Start()
    {
        CharacterAnimator = GetComponent<Animator>();
        Bow.GetComponent<BowController>().CreateArrow();
    }

    private void Update()
    {
        HandleShootArrow();
    }

    public void FinishShootAnim()
    {
        CharacterAnimator.SetBool("ShootArrow", false);
        CharacterAnimator.runtimeAnimatorController = simpleAnimatorController;
        drawControl = false;
    }

    private void DrawBow()
    {
        Bow.GetComponent<BowController>().DrawBow();
    }

    private void HandleShootArrow(){
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
                Bow.GetComponent<BowController>().shootArrow(10f);
                CharacterAnimator.SetBool("ShootArrow", true);
            }
        }           
    }
}
