using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float zoom = 80f;
    // public GameObject camera;
    void Update()
    {
        HandleZoom();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        if(transform.position.x < GameData.map_size_x/2 -20  && transform.position.x > -GameData.map_size_x/2 + 20 && transform.position.y < GameData.map_size_y/2 - 20 && transform.position.y > -GameData.map_size_y/2 + 20)
        {
            transform.position += movement * Time.deltaTime * 10;
        }
        else{
            transform.position += -movement * Time.deltaTime * 20;
        }
        
    }

    private void HandleZoom() {
        float zoomChangeAmount = 80f;
        if (Input.GetKey(KeyCode.KeypadPlus)){
            zoom -= zoomChangeAmount * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.KeypadMinus)){
            zoom += zoomChangeAmount * Time.deltaTime;
        }
         if (Input.mouseScrollDelta.y >0){
            zoom -= zoomChangeAmount * Time.deltaTime * 10;
        }
        if(Input.mouseScrollDelta.y <0){
            zoom += zoomChangeAmount * Time.deltaTime * 10;
        }

        zoom = Mathf.Clamp(zoom, 40f, 250f);
    }
}
