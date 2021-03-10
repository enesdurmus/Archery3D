using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private InputController inputController;

    private void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("Player").GetComponent<InputController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inputController.SetSprintInput(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputController.SetSprintInput(false);
    }
}
