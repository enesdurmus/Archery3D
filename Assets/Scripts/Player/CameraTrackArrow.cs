using UnityEngine;

public class CameraTrackArrow : MonoBehaviour
{
    private GameObject characterCamera;

    private GameObject TrackPos;

private GameObject arrow;
private GameObject cam1;
private GameObject player;
   public void Start()
    {
        characterCamera = GameObject.FindGameObjectWithTag("CharacterCamera");
        TrackPos = GameObject.FindGameObjectWithTag("TrackPos");
                cam1 = characterCamera.transform.Find("Main Camera").gameObject;
                                player = GameObject.FindGameObjectWithTag("Player");

    }

    public void TrackArrow()
    {
player.GetComponent<CharacterController>().enabled = false;
        //trackCamera.transform.rotation = Quaternion.Euler(verticalMouse, horizontalMouse, transform.eulerAngles.z);
        cam1.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z+5);

    }

}
