using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHintJoint : MonoBehaviour
{
    public HingeJoint hinge;
    JointMotor motor;
    public float velocity;
    public float angle;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        motor = hinge.motor;
    }

    
    void Update()
    {
        angle=hinge.angle;
        motor.targetVelocity = -angle;
        hinge.motor = motor;
    }
}
