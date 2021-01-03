using UnityEngine;

public class CameraTrackArrow : MonoBehaviour
{
    private GameObject characterCamera;
    private GameObject cam1;
    private GameObject player;
    private GameObject gameMode;

    public void Start()
    {
        characterCamera = GameObject.FindGameObjectWithTag("CharacterCamera");
        gameMode = GameObject.FindGameObjectWithTag("GameMode");
        cam1 = characterCamera.transform.Find("Main Camera").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        cam1.transform.position = Vector3.Lerp(cam1.transform.position, transform.position, 0.05f);
    }

    public void TrackArrow()
    {
        gameMode.GetComponent<SlowMotion>().DoSlowMotion();
        player.GetComponent<CharacterController>().enabled = false;
        //trackCamera.transform.rotation = Quaternion.Euler(verticalMouse, horizontalMouse, transform.eulerAngles.z);
    }
    public void ExitTrackArrow()
    {
        gameMode.GetComponent<SlowMotion>().ExitSlowMotion();
        player.GetComponent<CharacterController>().enabled = true;
    }

}
