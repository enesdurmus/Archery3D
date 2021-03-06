using UnityEngine;

public class InputController : MonoBehaviour
{
    private float vertical = 0f, horizontal = 0f;
    private float verticalMouse = 0f, horizontalMouse = 0f;
    private bool isMouseClicked;
    private bool isSprinting;
    private bool isPause;
 

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

    public bool GetPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) isPause = true;
        else isPause = false;

        return isPause;
    }

    public bool GetSprintInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) isSprinting = true;

        else if (Input.GetKeyUp(KeyCode.LeftShift)) isSprinting = false;

        return isSprinting;
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
