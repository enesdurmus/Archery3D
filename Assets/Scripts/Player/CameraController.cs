using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject characterCamera = null, crossHair = null;

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
        if (isMouseClicked)
        {
            iskelet.rotation = iskelet.rotation * Quaternion.Euler(new Vector3(0, 0, verticalMouse));
        } 
    }

    public void handleCameraMovement()
    {
        crossHair.SetActive(false);

        float[] cameraInputs = GetComponent<PlayerInput>().GetCameraInputs();
        verticalMouse = cameraInputs[0];
        horizontalMouse = cameraInputs[1];

        characterCamera.transform.rotation = Quaternion.Euler(verticalMouse, horizontalMouse, transform.eulerAngles.z);
        characterCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        float[] playerInputs = GetComponent<PlayerInput>().GetMovementInputs();
        isMouseClicked = GetComponent<PlayerInput>().GetMouseClickInf();

        if (isMouseClicked){
            crossHair.SetActive(true);
            cam1.transform.position = Vector3.Lerp(cam1.transform.position, pos2.transform.position, 0.015f);
            cam1.transform.rotation = Quaternion.Lerp(cam1.transform.rotation, pos2.transform.rotation, 0.01f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, horizontalMouse, transform.eulerAngles.z), 0.4f);
        }
        else if(playerInputs[1] != 0 || playerInputs[0] != 0){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, horizontalMouse, transform.eulerAngles.z), 0.4f);
        }
        else{
            cam1.transform.position = Vector3.Lerp(cam1.transform.position, pos1.transform.position, 0.015f);
            cam1.transform.rotation = Quaternion.Lerp(cam1.transform.rotation, pos1.transform.rotation, 0.01f);
        }
    }
}
