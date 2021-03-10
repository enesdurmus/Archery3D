using UnityEngine;
using System.Collections;


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
    private AudioSource[] audios;

    void Start()
    {
        currentHealt = maxHealt;
        healtBar.SetMaxHealt(maxHealt);
        attackPower = 10f;
        CharacterAnimator = GetComponent<Animator>();
        audios = GetComponents<AudioSource>();
        StartCoroutine(SetActiveCameraController(7f));
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            audios[6].Play();
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

    private IEnumerator SetActiveCameraController(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<CameraController>().enabled = true;
        GameObject.FindGameObjectWithTag("CharacterCamera").GetComponent<Animator>().enabled = false;
    }
    public void StopPlayerControl()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<CameraController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<ShootArrow>().enabled = false;
    }

    public void BeginPlayerControl()
    {
        GetComponent<CharacterController>().enabled = true;
        GetComponent<CameraController>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<ShootArrow>().enabled = true;
    }

    public void ReactAnimEnd()
    {
        CharacterAnimator.SetBool("ReactParam", false);
    }

    public void FinishDeadAnim()
    {
        CharacterAnimator.SetBool("isDead", false);
    }

    public void WalkSoundLeft()
    {
        audios[3].Play();
    }
    public void WalkSoundRight()
    {
        audios[2].Play();
    }

    public void RunSoundLeft() 
    {
        audios[4].Play();
    }
    public void RunSoundRight()
    {
        audios[5].Play();
    }
}