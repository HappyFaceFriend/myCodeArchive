using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoSize : MonoBehaviour
{
    Camera thisCamera;
    void Awake()
    {
        thisCamera = GetComponent<Camera>();
        //newsize = size / currentRatio * targetRatio
        thisCamera.orthographicSize = thisCamera.orthographicSize * (1920f/1080) / ((float)Screen.width/Screen.height);
    }
}