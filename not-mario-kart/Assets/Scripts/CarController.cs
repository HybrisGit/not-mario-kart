using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelController leftWheel;
    public WheelController rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    [SerializeField]
    private float _currentTorque;
    public float CurrentTorque
    {
        get
        {
            return this._currentTorque;
        }
        set
        {
            this._currentTorque = value;
            if (this._currentTorque > this.maxMotorTorque)
            {
                this._currentTorque = this.maxMotorTorque;
            }
        }
    }
    [SerializeField]
    private float _currentSteering;
    public float CurrentSteering
    {
        get
        {
            return this._currentSteering;
        }
        set
        {
            this._currentSteering = value;
            if (this._currentSteering > this.maxSteeringAngle)
            {
                this._currentSteering = this.maxSteeringAngle;
            }
        }
    }

    public void SetTorque(float torque)
    {
        this.CurrentTorque = torque * this.maxMotorTorque;
    }

    public void SetSteering(float angle)
    {
        this.CurrentSteering = angle * this.maxSteeringAngle;
    }

    public void FixedUpdate()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.Collider.steerAngle = this.CurrentSteering;
                axleInfo.rightWheel.Collider.steerAngle = this.CurrentSteering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.Collider.motorTorque = this.CurrentTorque;
                axleInfo.rightWheel.Collider.motorTorque = this.CurrentTorque;
            }
        }
    }
}