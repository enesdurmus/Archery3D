using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject characterCamera = null, crossHair = null;

    private float camGoSpeed = 0.05f, camBackSpeed = 0.1f;

    private float verticalMouse = 0, horizontalMouse = 0;
    private Transform iskelet;
    private bool isMouseClicked;
    private Animator CharacterAnimator;
    private GameObject cam1, pos1, pos2;

    private void Start()
    {
        cam1 = characterCamera.transform.Find("Main Camera").gameObject;
        pos1 = characterCamera.transform.Find("pos1").gameObject;
        pos2 = characterCamera.transform.Find("pos2").gameObject;
        CharacterAnimator = GetComponent<Animator>();
        iskelet = CharacterAnimator.GetBoneTransform(HumanBodyBones.Chest);
    }

    private void LateUpdate()
    {
        HandleCameraMovement();

        if (isMouseClicked)
        {
            iskelet.rotation *= Quaternion.Euler(new Vector3(0, 0, verticalMouse));
        }
    }

    public void HandleCameraMovement()
    {
        crossHair.SetActive(false);

        float[] cameraInputs = GetComponent<InputController>().GetCameraInputs();
        verticalMouse = cameraInputs[0];
        horizontalMouse = cameraInputs[1];

        characterCamera.transform.rotation = Quaternion.Euler(verticalMouse, horizontalMouse, transform.eulerAngles.z);
        characterCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        float[] playerInputs = GetComponent<InputController>().GetMovementInputs();
        isMouseClicked = GetComponent<InputController>().GetMouseClickInf();

        if (isMouseClicked)
        {
            crossHair.SetActive(true);
            cam1.transform.position = Vector3.Lerp(cam1.transform.position, pos2.transform.position, camGoSpeed);
            cam1.transform.rotation = Quaternion.Lerp(cam1.transform.rotation, pos2.transform.rotation, camGoSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, horizontalMouse, transform.eulerAngles.z), 0.4f);
        }
        else
        {
            cam1.transform.position = Vector3.Lerp(cam1.transform.position, pos1.transform.position, camBackSpeed);
            cam1.transform.rotation = Quaternion.Lerp(cam1.transform.rotation, pos1.transform.rotation, camBackSpeed);
        }
    }
}
