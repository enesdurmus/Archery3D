using UnityEngine;
using System.Collections;


public class CameraTrackArrow : MonoBehaviour
{
    private GameObject characterCamera;
    private GameObject cam, trackPos;
    private GameObject player;
    private GameObject gameMode;
    private bool isArrowTracking;
    private bool isHitEnemy;


    public void Start()
    {
        characterCamera = GameObject.FindGameObjectWithTag("CharacterCamera");
        gameMode = GameObject.FindGameObjectWithTag("GameMode");
        cam = characterCamera.transform.Find("Main Camera").gameObject;
        trackPos = transform.Find("TrackPos").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (isArrowTracking)
        {
            CamMove();
        }
        if (isHitEnemy)
        {
            CamMoveHit();
        }
    }

    private IEnumerator ExitTrack(float time)
    {
        gameMode.GetComponent<SlowMotion>().DoSlowMotion(0.5f);
        yield return new WaitForSeconds(time);
        gameMode.GetComponent<SlowMotion>().ExitSlowMotion();
        player.GetComponent<CameraController>().enabled = true;
        isHitEnemy = false;
    }

    private void CamMove()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, trackPos.transform.position, 0.05f);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, trackPos.transform.rotation, 0.01f);
    }

    private void CamMoveHit()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, trackPos.transform.position, 0.05f);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, trackPos.transform.rotation, 0.05f);
    }
    public void TrackArrow()
    {
        isArrowTracking = true;
        gameMode.GetComponent<SlowMotion>().DoSlowMotion();
        player.GetComponent<CameraController>().enabled = false;
    }
    public void ExitTrackArrow(GameObject trackPos)
    {
        if (isArrowTracking)
        {
            isArrowTracking = false;
            isHitEnemy = true;
            this.trackPos = trackPos;
            StartCoroutine(ExitTrack(1f));
        }
    }

    public void ExitTrackArrow()
    {
        if (isArrowTracking)
        {
            isArrowTracking = false;
            StartCoroutine(ExitTrack(0f));
        }
    }
}
