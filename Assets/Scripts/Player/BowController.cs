using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject arrowPos;

    private bool isBowDraw = true;
    private int arrowCounter = 0;
    private GameObject[] arrowPool;

    public GameObject[] CreateArrow()
    {
        arrowPool = new GameObject[12];
        for (int i = 0; i < 12; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            arrowPool[i] = arrow;
        }
        return arrowPool;
    }

    public void DrawBow()
    {
        if (isBowDraw) {
            arrowCounter = 0;

            for (int i = 0; i < arrowPool.Length; i++)
            {
                if (arrowPool[i].activeSelf == false)
                {
                    arrowCounter = i;
                    break;
                }
            }
            isBowDraw = false;
        }

        arrowPool[arrowCounter].transform.position = arrowPos.transform.position;
        arrowPool[arrowCounter].transform.rotation = arrowPos.transform.rotation;
        arrowPool[arrowCounter].SetActive(true);
    }

    public void shootArrow(float attackPower)
    {
        arrowPool[arrowCounter].GetComponent<ArrowController>().InputUpdates(attackPower);
        isBowDraw = true;
    }
}