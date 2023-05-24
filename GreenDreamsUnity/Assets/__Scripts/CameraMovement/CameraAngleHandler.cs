using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleHandler : MonoBehaviour
{
     [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private Cinemachine3rdPersonFollow targetCamera;
   
    public float desiredVerticalArm;

    private void Awake()
    {
        targetCamera = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        targetCamera.CameraDistance = 2f;
        desiredVerticalArm = 0.4f;
    }
    void FixedUpdate()
    {
       
        if (targetCamera.CameraDistance > 4f)
        {
            desiredVerticalArm = 2f;
           targetCamera.VerticalArmLength = Mathf.Lerp(targetCamera.VerticalArmLength, desiredVerticalArm, 0.05f);
        }
        else if ( targetCamera.CameraDistance <= 4f )
        {
            desiredVerticalArm = 0.4f;
           targetCamera.VerticalArmLength= Mathf.Lerp(targetCamera.VerticalArmLength, desiredVerticalArm, 0.05f);
            
        }
    }
}
