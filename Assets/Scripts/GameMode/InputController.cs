using UnityEngine;

public class InputController : MonoBehaviour
{
    public Joystick joystickMovement = null;
    public Joystick joystickCamera = null;
    public Joystick joystickAim = null;

    private Joystick tempJoystick;

    private float cameraSens = 70f;

    private float vertical = 0f, horizontal = 0f;
    private float verticalMouse = 0f, horizontalMouse = 0f;
    private bool isMouseClicked;
    private bool isSprinting;

    private void Start()
    {
        tempJoystick = joystickCamera;
    }
    public float[] GetMovementInputs()
    {
        // vertical = Input.GetAxis("Vertical");
        // horizontal = Input.GetAxis("Horizontal");

        vertical = joystickMovement.Vertical;
        horizontal = joystickMovement.Horizontal;

        float[] inputs = { vertical, horizontal };
        return inputs;
    }

    public float[] GetCameraInputs()
    {
        //For Pc
        //verticalMouse += Input.GetAxisRaw("Mouse Y") * -3f;
        //horizontalMouse += Input.GetAxisRaw("Mouse X") * 3f;


        //For Touch
        /*  if (Input.touchCount > 0)
          {
              touch = Input.GetTouch(0);

              if (touch.phase == TouchPhase.Moved)
              {
                  verticalMouse += touch.deltaPosition.y * Time.deltaTime * -15;
                  horizontalMouse += touch.deltaPosition.x * Time.deltaTime * 15;
              }
          }*/

        //For joystick
        verticalMouse += joystickCamera.Vertical * -cameraSens * Time.deltaTime;
        horizontalMouse += joystickCamera.Horizontal * cameraSens * Time.deltaTime;


        verticalMouse = Mathf.Clamp(verticalMouse, -20, 10);

        float[] inputs = { verticalMouse, horizontalMouse };
        return inputs;
    }

    public bool GetSprintInput()
    {
        return isSprinting;
    }

    public void SetSprintInput(bool state)
    {
        isSprinting = state;
    }

    public bool GetMouseClickInf()
    {
        //For pc
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isMouseClicked = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isMouseClicked = false;
        }*/

        if (Mathf.Abs(joystickAim.Vertical + joystickAim.Horizontal) >= 0.0001f)
        {
            isMouseClicked = true;
            joystickCamera = joystickAim;
            cameraSens = 20f;
        }
        else
        {
            joystickCamera = tempJoystick;
            isMouseClicked = false;
            cameraSens = 70f;

        }

        return isMouseClicked;
    }
}
