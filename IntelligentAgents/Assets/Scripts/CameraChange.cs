using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera[] cameras;

    private int updateTick = 0;
    private int currentCameraIndex;

    void Start() {
        currentCameraIndex = 0;

        cameras = Camera.allCameras;
        SetLiveDataToZero();

        for (int i=1; i<cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        
        if (cameras.Length >0)
        {
            cameras[0].gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentCameraIndex++;
            if(currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex-1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
            }
            else
            {
                cameras[currentCameraIndex-1].gameObject.SetActive(false);
                currentCameraIndex =0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
                SetLiveDataToZero();
            }
            cameras[currentCameraIndex].GetComponent<CameraFollow>().UpdateLiveData();
        }
        if(Input.GetKeyDown("space"))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
            SetLiveDataToZero();
        }
        updateTick++;
        if(updateTick == 10)
        {
            cameras[currentCameraIndex].GetComponent<CameraFollow>().UpdateLiveData();
            updateTick = 0;
        }
    }   

    void SetLiveDataToZero()
    {
         LiveData.Agent_Name = null;
        LiveData.Agent_Gold = 0;
        LiveData.Agent_Energy = 0;
        LiveData.Agent_Rock = 0;
        LiveData.Agent_Wood = 0;
        LiveData.Agent_Village = null;
        LiveData.Agent_Pots = 0;
    }

    
}
