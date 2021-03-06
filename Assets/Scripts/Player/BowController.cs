using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject arrowPos;

    private Animator bowAnimator;

    GameObject arrow;

    private void Start()
    {
        bowAnimator = GetComponent<Animator>();
    }

    public GameObject CreateArrow()
    {
        arrow = Instantiate(arrowPrefab);
        arrow.SetActive(false);

        return arrow;
    }

    public void StartDrawAnim()
    {
        bowAnimator.SetBool("drawAnim", true);
    }

    public void StartReleaseAnim()
    {
        bowAnimator.SetBool("drawAnim", false);
    }
    public void DrawBow()
    {
        arrow.transform.position = arrowPos.transform.position;
        arrow.transform.rotation = arrowPos.transform.rotation;
        arrow.SetActive(true);
    }

    public void ShootArrow(float attackPower)
    {
        if (arrow != null)
        {
            arrow.GetComponent<ArrowController>().InputUpdates(attackPower);
            StartReleaseAnim();
        }
    }
}