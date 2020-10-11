using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float vertical = 0f, horizontal = 0f;
    private float verticalMouse = 0f, horizontalMouse = 0f;
    private bool isMouseClicked;   
 

    public float[] GetMovementInputs()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        float[] inputs = {vertical, horizontal};
        return inputs;
    }

    public float[] GetCameraInputs()
    {
        verticalMouse += Input.GetAxisRaw("Mouse Y") * -3f;
        horizontalMouse += Input.GetAxisRaw("Mouse X") * 3f;
        verticalMouse = Mathf.Clamp(verticalMouse, -20, 10);

        float[] inputs = {verticalMouse, horizontalMouse };
        return inputs;
    }

    public bool GetMouseClickInf()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isMouseClicked = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isMouseClicked = false;
        }
        return isMouseClicked;
    }
}
