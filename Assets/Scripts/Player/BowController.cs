using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject arrowPos;

    GameObject arrow;

    public GameObject CreateArrow()
    {
        arrow = Instantiate(arrowPrefab);
        arrow.SetActive(false);

        return arrow;
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
        }
    }
}