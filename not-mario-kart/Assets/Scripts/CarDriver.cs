using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController), typeof(EmotionController))]
public class CarDriver : MonoBehaviour
{
    [HideInInspector]
    public CarController carController;
    [HideInInspector]
    public EmotionController emotionController;

    public bool isDriving;
    public Checkpoint nextCheckpoint;

    void Awake()
    {
        this.carController = this.GetComponent<CarController>();
    }

    void Start()
    {
        DriverHandler.Instance.Register(this);
    }

    void Update()
    {
        if (!this.isDriving)
            return;

        this.TryReachNextCheckpoint();
    }

    void OnTriggerEnter(Collider other)
    {
        // if not next checkpoint
        Checkpoint checkpointInOther = other.GetComponent<Checkpoint>();
        if (checkpointInOther != this.nextCheckpoint)
        {
            return;
        }

        // claim checkpoint
        this.nextCheckpoint = this.nextCheckpoint.nextCheckpoint;
    }

    private static void AbsMin(float value, float min)
    {
        if (value >= 0f && value < min)
        {
            return min;
        }
        if (value <= 0f && value > -min)
        {
            return -min;
        }
    }

    private void TryReachNextCheckpoint()
    {
        if (this.nextCheckpoint == null)
            return;

        // calculate difference in position
        Vector3 distanceToTarget = this.nextCheckpoint.transform.position - this.transform.position;
        Vector3 directionToTarget = normalize(distanceToTarget);

        // project onto forward/right vectors
        float alongForward = Vector3.Dot(directionToTarget, this.transform.forward);
        float alongRight = Vector3.Dot(directionToTarget, this.transform.right);

        // accelerate forward/backward
        float torque = AbsMin(alongForward, 0.25f);
        this.carController.CurrentTorque = alongForward;

        // steer
        float steer = AbsMin(alongRight, 0.25f);
        this.carController.CurrentSteering = steer;
    }
}
