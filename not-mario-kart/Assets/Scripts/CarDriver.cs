using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController), typeof(GameCharacterController))]
public class CarDriver : MonoBehaviour
{
    [System.Serializable]
    public struct DriverSettings
    {
        public float throttleMin;
        public float forwardPreference;
    }

    [HideInInspector]
    public CarController carController;
    [HideInInspector]
    public GameCharacterController characterController;

    public DriverSettings driverSettings;
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
        //Debug.Log("Car driver hit trigger " + other.gameObject.name + " with checkpoint " + checkpointInOther + ". Is next : " + (checkpointInOther == this.nextCheckpoint));
        if (checkpointInOther != this.nextCheckpoint && this.nextCheckpoint != null)
        {
            return;
        }

        // claim checkpoint
        this.nextCheckpoint = checkpointInOther.nextCheckpoint;
    }

    private static float AbsMin(float value, float min)
    {
        if (value >= 0f && value < min)
        {
            return min;
        }
        if (value <= 0f && value > -min)
        {
            return -min;
        }

        return value;
    }

    private void TryReachNextCheckpoint()
    {
        if (this.nextCheckpoint == null)
            return;

        // calculate difference in position
        Vector3 distanceToTarget = this.nextCheckpoint.transform.position - this.transform.position;
        Vector3 directionToTarget = Vector3.Normalize(distanceToTarget);

        // project onto forward/right vectors
        float alongForward = Vector3.Dot(directionToTarget, this.transform.forward);
        float alongRight = Vector3.Dot(directionToTarget, this.transform.right);

        // accelerate forward/backward
        float torque = AbsMin(alongForward + this.driverSettings.forwardPreference, this.driverSettings.throttleMin);
        this.carController.SetTorque(alongForward);

        // steer
        float steer = AbsMin(alongRight, 0.0f);
        this.carController.SetSteering(steer);
    }
}
