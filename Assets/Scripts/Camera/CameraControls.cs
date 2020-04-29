using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    public Camera firstPersonCam;
    public Camera thirdPersonCam;
    public Camera birdPersonCam;
    public GameObject thirdPersonplayer;
    public GameObject firstPersonplayer;

    public void SetToFirstPerson(){
        firstPersonCam.enabled = true;
        firstPersonplayer.SetActive(true);
        thirdPersonCam.enabled = false;
        thirdPersonplayer.SetActive(false);
        birdPersonCam.enabled = false;
    }

    public void SetToThirdPerson(){
        firstPersonCam.enabled = false;
        firstPersonplayer.SetActive(false);
        thirdPersonCam.enabled = true;
        thirdPersonplayer.SetActive(true);
        birdPersonCam.enabled = false;

    }

    public void setToBirdPerson() {
        firstPersonCam.enabled = false;
        firstPersonplayer.SetActive(false);
        thirdPersonCam.enabled = false;
        thirdPersonplayer.SetActive(false);
        birdPersonCam.enabled = true;
    }

    private void Start()
    {
        setToBirdPerson();
    }

}