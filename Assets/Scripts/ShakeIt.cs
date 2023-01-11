﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeIt : MonoBehaviour
{
    public static ShakeIt instance;
    Vector3 cameraInitialPosition;
    public float shakeMagnitude = 0.06f, shakeTime = 0.5f;
    public Camera mainCamera;


    public void Start()
    {
        Shakeit();
    }

    public void Shakeit()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShake", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    void StopCameraShake()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }

}

